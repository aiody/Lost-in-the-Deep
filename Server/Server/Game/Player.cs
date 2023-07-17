using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Player
    {
        public int Id { get; set; }
        public GameRoom Room { get; set; }
        public ClientSession Session { get; set; }

        public CharacterType type { get; set; }
        public int depth { get; set; }
        public int fuel { get; set; }
        public int food { get; set; }
        public int oxygen { get; set; }
        public int relic { get; set; }
    }
}
