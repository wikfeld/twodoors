using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoDoors.Models;

namespace TwoDoors.Services
{
    /// <summary>
    /// Static data factory
    /// </summary>
    public class StaticDataFactory
    {
        public IDoorAccessTokenRepository CreateTokenRepository()
        {
            var tokens = new[] { new DoorAccessToken()
            {
                Id = 1,
                DoorId = 1,
                Revoked = false,
                Secret = "Open Sesame"
            },
            new DoorAccessToken()
            {
                Id = 2,
                DoorId = 1,
                Revoked = false,
                Secret = "mellon"
            },
            new DoorAccessToken()
            {
                Id = 3,
                DoorId = 2,
                Revoked = false,
                Secret = "p455w0rd"
            }
            };
            return new StaticTokenRepository(tokens);
        }
    }
}
