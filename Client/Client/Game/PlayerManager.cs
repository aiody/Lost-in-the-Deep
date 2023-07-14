using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class PlayerManager
    {
        public static PlayerManager Instance { get; } = new PlayerManager();

        public Player MyPlayer { get; private set; }
        public Dictionary<int, Player> Players { get; private set; } = new Dictionary<int, Player>();

        public Player Add(int playerId, bool isYourself = false)
        {
            Player player = new Player();
            player.Id = playerId;

            if (isYourself)
                MyPlayer = player;
            else
                Players.Add(player.Id, player);

            return player;
        }

        public bool Remove(int playerId)
        {
            return Players.Remove(playerId);
        }

        public Player Find(int playerId)
        {
            Player player = null;
            Players.TryGetValue(playerId, out player);
            return player;
        }
    }
}
