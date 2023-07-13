using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    internal class PlayerManager
    {
        public static PlayerManager Instance { get; } = new PlayerManager();

        object _lock = new object();
        Dictionary<int, Player> _players = new Dictionary<int, Player>();

        int _counter = 1;

        public Player Add()
        {
            Player player = new Player();

            lock (_lock)
            {
                player.Id = _counter++;
                _players.Add(player.Id, player);
            }

            return player;
        }

        public bool Remove(int playerId)
        {
            lock (_lock)
            {
                return _players.Remove(playerId);
            }
        }

        public Player Find(int playerId)
        {
            lock (_lock)
            {
                Player player = null;
                _players.TryGetValue(playerId, out player);
                return player;
            }
        }
    }
}
