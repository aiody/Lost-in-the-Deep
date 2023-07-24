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

            int id = 0;
            if (int.TryParse(r["id"], out id) == false)
            {
                Console.WriteLine("Id has invalid value");
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
                    Action action = ParseAction(r);
                    
                    if (action != null)
                        actions.Add(action);
                }
            }

            Event newEvent = new Event
            {
                Id = id,
                Name = eventName,
                Stage = stage,
                Description = description
            };

            foreach (Action a in actions)
                newEvent.Actions.Add(a);

            return newEvent;
        }

        Action ParseAction(XmlReader r)
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

            int id = 0;
            if (int.TryParse(r["id"], out id) == false)
            {
                Console.WriteLine("Id has invalid value");
                return null;
            }

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

            return new Action
            {
                Id = id,
                Name = actionName,
                Description = actionDesc,
                Surge = surge,
                Fuel = fuel,
                Food = food,
                Oxygen = oxygen,
                Relic = relic
            };
        }
    }
}
