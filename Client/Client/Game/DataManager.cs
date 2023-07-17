using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class DataManager
    {
        public static DataManager Instance { get; private set; } = new DataManager();

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
