using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Services
{

    /// <summary>
    /// A contract containing the access control methods for doors.
    /// </summary>
    public interface IDoorAccessControl
    {
        /// <summary>
        /// Verifies if the door can be open using the given secret.
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        bool CanOpen(int doorId, string secret);
    }
}
