using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    /// <summary>
    /// A contract to retrieve access tokens.
    /// </summary>
    public interface IDoorAccessTokenRepository
    {
        /// <summary>
        /// Finds the first valid (not revoked) access token for the given door.
        /// A null reference is returned when no valid token exists.
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        DoorAccessToken Get(int doorId, string secret);

        /// <summary>
        /// Creates an access token for the given door.
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="secret"></param>
        /// <returns>True if the token was successfully created.</returns>
        bool Issue(int doorId, string secret);

        /// <summary>
        /// Revokes all access token for the given door that use the secret.
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="secret"></param>
        /// <returns>True if all tokens were successfulyl revoked.</returns>
        bool Revoke(int doorId, string secret);
    }
}
