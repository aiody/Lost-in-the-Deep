using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    struct Action
    {
        public string name;
        public string description;
        public int surge;
        public int fuel;
        public int food;
        public int oxygen;
        public int relic;
    }

    internal class Event
    {
        public string Name { get; private set; }
        public int Stage { get; private set; }
        public string Description { get; private set; }
        public List<Action> Actions { get; private set; }

        public Event(string name, int stage, string description, List<Action> actions)
        {
            Name = name;
            Stage = stage;
            Description = description;
            Actions = actions;
        }
    }
}
