using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public class TwoDoorsRepository : IDoorRepository
    {
        private Dictionary<int, Door> _doors = new Dictionary<int, Door>()
        {
            { 1, new Door() { Id = 1, Name = "Gate"} },
            { 2, new Door() { Id = 2, Name = "Office"} }
        };

        public Door Get(int id)
        {
            return _doors.ContainsKey(id) ? _doors[id] : null;
        }
    }
}
