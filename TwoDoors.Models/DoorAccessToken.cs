using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{

    /*
     # About the tokens:

     ## Tokens are secrets that grant access to a door. The token is a pair (door, secret). A secret must be used on a known door.

     ## Two tokens for the same door can have the same secret.

        Not a problem when both are valid.
        A problem when one is revoked and the other one is not.        

     ## Two tokens for different doors can have the same secret.

        This presents a problem when using sequential door identifiers. 
        It is possible to enumerate doors and try weak secrets on all of them hoping that some door will open.

        Possible solutions:

        Don't use door ids that can be enumerated
        Bind the token to a user of group of users. Right now the solution assumes that all users are in the Anonymous group.

    
     ## Secrets are plain text passwords (chosen by a person or randomly generated)

        If we are implementing a solution that is esentially "passwords for doors", 
        then proper password storage techniques should be used such as not storing the plain text secret 
        and instead store salted hashes.

        For simplicity this solution saves plain text secrets.

     */

    /// <summary>
    /// A token that can grant access to a door.
    /// </summary>
    public class DoorAccessToken
    {
        public int Id { get; set; }
        /// <summary>
        /// The secret that grants access to the door.
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// The Id of the door that the token grants access to.
        /// </summary>
        public int DoorId { get; set; }
        /// <summary>
        /// True when access has been revoked for the token.
        /// </summary>
        public bool Revoked { get; set; }
    }
}
