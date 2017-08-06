using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoDoors.Models;

namespace TwoDoors.Services
{
    public class DoorAccessControl : IDoorAccessControl
    {
        private IDoorRepository _doors;
        private IDoorAccessTokenRepository _tokens;

        public DoorAccessControl(IDoorRepository doors, IDoorAccessTokenRepository tokens)
        {
            _doors = doors;
            _tokens = tokens;
        }

        public bool CanOpen(int doorId, string secret)
        {
            var door = _doors.Get(doorId);
            // door cannot be found and cannot be open
            if(door == null) { return false; }

            var token = _tokens.Get(doorId, secret);
            // a token was found
            if(token != null) { return true; }

            // reject otherwise
            return false;
        }
    }
}
