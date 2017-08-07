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

            // cannot open a door that is not found
            if(door == null) { return false; }

            var token = _tokens.Get(doorId, secret);

            // deny access when the token is revoked
            // deny access when the token is not for this door
            // deny access if the secret does not match token
            if(token != null 
                && !token.Revoked 
                && token.DoorId == doorId
                && token.Secret == secret) { return true; }

            // reject otherwise
            return false;
        }
    }
}
