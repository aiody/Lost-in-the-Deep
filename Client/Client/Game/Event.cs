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
    }

    internal class Event
    {
        string _name;
        int _depth;
        string _description;
        List<Action> _actions;

        public Event(string name, int depth, string description, List<Action> actions)
        {
            _name = name;
            _depth = depth;
            _description = description;
            _actions = actions;
        }
    }
}
