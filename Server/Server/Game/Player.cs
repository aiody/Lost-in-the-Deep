using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Google.Protobuf.Protocol.Action;

namespace Server
{
    internal class Player
    {
        public int Id { get; set; }
        public GameRoom Room { get; set; }
        public ClientSession Session { get; set; }

        public string PlayerName { get; set; }

        public CharacterType type { get; set; }
        public int depth { get; set; }
        public int fuel { get; set; }
        public int food { get; set; }
        public int oxygen { get; set; }
        public int relic { get; set; }

        public void ApplyActionResult(Action targetAction)
        {
            depth -= targetAction.Surge;
            fuel += targetAction.Fuel;
            food += targetAction.Food;
            oxygen += targetAction.Oxygen;
            relic += targetAction.Relic;

            if (depth <= 0 || fuel <= 0 || food <= 0 || oxygen <= 0)
            {
                Room.LeaveGame(Id);
            }
        }
    }
}
