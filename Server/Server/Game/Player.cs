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
    }
}
