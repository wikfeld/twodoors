using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    /// <summary>
    /// A door.
    /// </summary>
    public class Door
    {
        /// <summary>
        /// The door unique identifier.        
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// A description for the door.
        /// </summary>
        public string Name { get; set; }
    }
}
