using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDoors.Models
{
    public class StaticTokenRepository : IDoorAccessTokenRepository
    {
        // dictionary of (door id, list of tokens for door)
        private readonly Dictionary<int, IList<DoorAccessToken>> _tokens = new Dictionary<int, IList<DoorAccessToken>>();

        private int _nextId;

        public StaticTokenRepository()
        {
            _nextId = 1;
        }

        public StaticTokenRepository(IEnumerable<DoorAccessToken> tokens)
        {
            // populate the repository with some tokens
            (tokens ?? Enumerable.Empty<DoorAccessToken>()).ToList().ForEach(x => Add(x));
            _nextId = tokens == null ? 1 : tokens.Max(x => x.Id) + 1;
        }

        public DoorAccessToken Get(int doorId, string secret)
        {
            if (!_tokens.ContainsKey(doorId)) return null;
            return _tokens[doorId].FirstOrDefault(x => x.Secret == secret && !x.Revoked);
        }

        public bool Issue(int doorId, string secret)
        {
            var token = new DoorAccessToken()
            {
                Id = _nextId,
                DoorId = doorId,
                Secret = secret,
                Revoked = false
            };

            _nextId = _nextId + 1;

            Add(token);

            return true;
        }

        public bool Revoke(int doorId, string secret)
        {
            foreach(var token in GetAll(doorId, secret))
            {
                token.Revoked = true;
            }
            return true;
        }

        /// <summary>
        /// Find all valid tokens for the door that use the given secret.
        /// </summary>
        /// <param name="doorId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        private IEnumerable<DoorAccessToken> GetAll(int doorId, string secret)
        {
            if (!_tokens.ContainsKey(doorId)) return Enumerable.Empty<DoorAccessToken>();
            return _tokens[doorId].Where(x => x.Secret == secret && !x.Revoked);
        }

        /// <summary>
        /// Adds a token to the dictionary
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool Add(DoorAccessToken token)
        {
            if (!_tokens.ContainsKey(token.DoorId))
            {
                _tokens.Add(token.DoorId, new List<DoorAccessToken>());
            }

            _tokens[token.DoorId].Add(token);

            return true;
        }
    }
}
