using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    /// <summary>
    /// A contract to query doors.
    /// </summary>
    public interface IDoorRepository
    {
        /// <summary>
        /// Finds the door using its identifier.
        /// A null reference is returned when no such door is found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Door Get(int id);
    }
}
