using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Player
    {
        public int Id { get; set; }

        public Character _character;
        public int _depth;
        public int _fuel;
        public int _food;
        public int _oxygen;
        public int _relic;

        public string icon = "●";
        public string name = "";
    }
}
