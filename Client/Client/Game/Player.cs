using Google.Protobuf.Protocol;
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

        public CharacterType? Character { get; set; }
        public string CharacterName
        {
            get
            {
                switch (Character)
                {
                    case CharacterType.Diver:
                        return "다이버";
                    case CharacterType.MarineBiologist:
                        return "해양생물학자";
                    case CharacterType.Archaeologist:
                        return "고고학자";
                    default:
                        return "";
                }
            }
        }
        public int Depth { get; set; }
        public int Fuel { get; set; }
        public int Food { get; set; }
        public int Oxygen { get; set; }
        public int Relic { get; set; }

        public string icon = "●";
        public string name = "";
    }
}
