using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class DataManager
    {
        public static List<Event> Events { get; private set; } = new List<Event>();

        public static void LoadData()
        {
            EventLoader eventLoader = new EventLoader();
            Events = eventLoader.Load();
        }
    }
}
