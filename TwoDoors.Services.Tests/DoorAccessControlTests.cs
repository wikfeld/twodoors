using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwoDoors.Models;

namespace TwoDoors.Services.Tests
{
    [TestClass]
    public class DoorAccessControlTests
    {
        [TestMethod]
        public void WhenThereIsADoorAndValidToken_ExpectAccessGranted()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.Is<string>(y => y == secret)))
                .Returns(new DoorAccessToken() { DoorId = doorId, Id = 1, Revoked = false, Secret = secret });

            var result = test.TestClass.CanOpen(doorId, secret);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenThereIsADoorAndRevokedToken_ExpectAccessGranted()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.Is<string>(y => y == secret)))
                // this mock setup does not follow the token repository contract (no revoked tokens shall be returned)
                .Returns(new DoorAccessToken() { DoorId = doorId, Id = 1, Revoked = true, Secret = secret });

            // the test continues and denies access because this IDoorAccessControl 
            // implementation double checks the tokens returned by the repository.
            var result = test.TestClass.CanOpen(doorId, secret);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenThereAreMultipleTokens_ExpectAccessGrantedForAll()
        {
            var doorId = 1;
            string secret1 = "mysecret1";
            string secret2 = "mysecret2";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.IsAny<string>()))
                // returns a valid token for every secret
                .Returns<int, string>((_, s) => new DoorAccessToken() { DoorId = doorId, Id = 1, Revoked = false, Secret = s });
           
            Assert.IsTrue(test.TestClass.CanOpen(doorId, secret1));
            Assert.IsTrue(test.TestClass.CanOpen(doorId, secret2));
        }

        [TestMethod]
        public void WhenSecretDoesNotMatchToken_ExpectAccessDenied()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.Is<string>(y => y == secret)))
                .Returns(new DoorAccessToken() { DoorId = doorId, Id = 1, Revoked = false, Secret = "anothersecret" });

            var result = test.TestClass.CanOpen(doorId, secret);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhentheTokenIsNotForTheDoor_ExpectAccessDenied()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.Is<string>(y => y == secret)))
                .Returns(new DoorAccessToken() { DoorId = 2, Id = 1, Revoked = false, Secret = secret });

            var result = test.TestClass.CanOpen(doorId, secret);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenThereIsNoDoor_ExpectAccessDenied()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Tokens
                .Setup(x => x.Get(It.IsAny<int>(), It.Is<string>(y => y == secret)))
                .Returns(new DoorAccessToken() { DoorId = doorId, Id = 1, Revoked = false, Secret = secret });

            var result = test.TestClass.CanOpen(doorId, secret);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenThereIsNoToken_ExpectAccessDenied()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            test.Doors
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new Door() { Id = doorId });

            var result = test.TestClass.CanOpen(doorId, secret);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenThereIsNoDoorAndNoToken_ExpectAccessDenied()
        {
            var doorId = 1;
            string secret = "mysecret";

            var test = new TestSetup();
            var result = test.TestClass.CanOpen(doorId, secret);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Creates mocks and initializes the class under test.
        /// </summary>
        private class TestSetup
        {
            public Mock<IDoorRepository> Doors { get; set; }
            public Mock<IDoorAccessTokenRepository> Tokens { get; set; }
            public DoorAccessControl TestClass { get; set; }

            public TestSetup()
            {
                Doors = new Mock<IDoorRepository>();
                Tokens = new Mock<IDoorAccessTokenRepository>();
                TestClass = new DoorAccessControl(Doors.Object, Tokens.Object);
            }
        }
    }
}
