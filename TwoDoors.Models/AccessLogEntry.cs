using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public class AccessLogEntry
    {
        /// <summary>
        /// Log entry identifier.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Door identifier.
        /// </summary>
        public int DoorId { get; set; }
        /// <summary>
        /// When the access attempt took place.
        /// </summary>
        public DateTime AccessTimestamp { get; set; }
        /// <summary>
        /// Did the access attempt resulted in access granted? Or was it rejected?
        /// </summary>
        public bool AccessGranted { get; set; }
    }
}
