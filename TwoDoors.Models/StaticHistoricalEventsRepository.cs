using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public class StaticHistoricalEventsRepository : IHistoricalEventsRepository
    {
        private List<HistoricalEvent> _entries = new List<HistoricalEvent>();
        private int _nextId;
        private ITimeProvider _time;

        public StaticHistoricalEventsRepository(ITimeProvider time)
        {
            _time = time;
            _nextId = 1;
        }

        public void Add(int doorId, bool accessGranted)
        {
            var entry = new HistoricalEvent()
            {
                AccessGranted = accessGranted,
                AccessTimestamp = _time.CurrentTime(),
                DoorId = doorId,
                Id = _nextId
            };
            _entries.Add(entry);
            _nextId = _nextId + 1;
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public IEnumerable<HistoricalEvent> GetAll(int doorId)
        {
            return _entries
                .Where(x => x.DoorId == doorId)
                .OrderByDescending(x => x.AccessTimestamp);
        }
    }
}
