using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    /// <summary>
    /// Contract to keep a log of when doors are open
    /// </summary>
    public interface IHistoricalEventsRepository
    {
        /// <summary>
        /// Log an access attempt and its success
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="accessGranted"></param>
        void Add(int doorId, bool accessGranted);

        /// <summary>
        /// Find all log entries for the given door
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        IEnumerable<HistoricalEvent> GetAll(int doorId);

        // The clear method is here to reset the repository 
        // since the application will be running with a long lived in-memory implementation.
        /// <summary>
        /// Clears the log.
        /// </summary>
        void Clear();
    }
}
