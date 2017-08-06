using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public class StaticAccessLogRepository : IAccessLogRepository
    {
        private List<AccessLogEntry> _entries = new List<AccessLogEntry>();
        private int _nextId;
        private ITimeProvider _time;

        public StaticAccessLogRepository(ITimeProvider time)
        {
            _time = time;
            _nextId = 1;
        }

        public void Add(int doorId, bool accessGranted)
        {
            var entry = new AccessLogEntry()
            {
                AccessGranted = accessGranted,
                AccessTimestamp = _time.CurrentTime(),
                DoorId = doorId,
                Id = _nextId
            };
            _entries.Add(entry);
            _nextId = _nextId + 1;
        }

        public IEnumerable<AccessLogEntry> GetAll(int doorId)
        {
            return _entries
                .Where(x => x.DoorId == doorId)
                .OrderByDescending(x => x.AccessTimestamp);
        }
    }
}
