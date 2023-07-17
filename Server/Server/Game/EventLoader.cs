using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Action = Google.Protobuf.Protocol.Action;

namespace Server
{
    internal class EventLoader
    {
        public List<Event> Load()
        {
            string eventsPath = "../../../Events.xml";
            List<Event> events = new List<Event>();

            XmlReaderSettings settings = new XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };

            using (XmlReader r = XmlReader.Create(eventsPath, settings))
            {
                r.MoveToContent();

                while (r.Read())
                {
                    if (r.Depth == 1 && r.NodeType == XmlNodeType.Element)
                    {
                        Event e = ParseEvent(r);
                        if (e != null)
                            events.Add(e);
                    }
                }
            }

            return events;
        }

        Event ParseEvent(XmlReader r)
        {
            if (r.Name.ToLower() != "event")
            {
                Console.WriteLine("Invalid event node");
                return null;
            }

            string eventName = r["name"];
            if (string.IsNullOrEmpty(eventName))
            {
                Console.WriteLine("Event without name");
                return null;
            }

            int stage = 0;
            if (int.TryParse(r["stage"], out stage) == false)
            {
                Console.WriteLine("Event has invalid stage");
                return null;
            }

            string description = "";
            List<Action> actions = new List<Action>();
            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.EndElement && r.Name == "event")
                    break;

                if (r.NodeType != XmlNodeType.Element)
                    continue;

                if (r.Name == "description")
                {
                    r.Read();
                    description = r.Value;
                }
                if (r.Name == "action")
                {
                    Tuple<string, string, int, int, int, int, int> t = ParseAction(r);
                    Action action = new Action
                    {
                        Name = t.Item1,
                        Description = t.Item2,
                        Surge = t.Item3,
                        Fuel = t.Item4,
                        Food = t.Item5,
                        Oxygen = t.Item6,
                        Relic = t.Item7
                    };
                    if (t != null)
                        actions.Add(action);
                }
            }

            Event newEvent = new Event
            {
                Name = eventName,
                Stage = stage,
                Description = description
            };

            foreach (Action a in actions)
                newEvent.Actions.Add(a);

            return newEvent;
        }

        Tuple<string, string, int, int, int, int, int> ParseAction(XmlReader r)
        {
            if (r.Name.ToLower() != "action")
            {
                Console.WriteLine("Invalid action node");
                return null;
            }

            string actionDesc = "";
            int surge = 0;
            int fuel = 0;
            int food = 0;
            int oxygen = 0;
            int relic = 0;

            string actionName = r["name"];
            if (string.IsNullOrEmpty(actionName))
            {
                Console.WriteLine("Action without name");
                return null;
            }

            while (r.Read())
            {
                if (r.NodeType == XmlNodeType.EndElement && r.Name == "action")
                    break;

                if (r.NodeType != XmlNodeType.Element)
                    continue;

                if (r.Name == "description")
                {
                    r.Read();
                    actionDesc = r.Value;
                }

                if (r.Name == "surge")
                {
                    r.Read();
                    int.TryParse(r.Value, out surge);
                }

                if (r.Name == "fuel")
                {
                    r.Read();
                    int.TryParse(r.Value, out fuel);
                }

                if (r.Name == "food")
                {
                    r.Read();
                    int.TryParse(r.Value, out food);
                }

                if (r.Name == "oxygen")
                {
                    r.Read();
                    int.TryParse(r.Value, out oxygen);
                }

                if (r.Name == "relic")
                {
                    r.Read();
                    int.TryParse(r.Value, out relic);
                }
            }

            return new Tuple<string, string, int, int, int, int, int>(actionName, actionDesc, surge, fuel, food, oxygen, relic);
        }
    }
}
