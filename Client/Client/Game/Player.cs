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
        public int Id
        {
            get { return Info.PlayerId; }
            set { Info.PlayerId = value; }
        }

        public Stat Stat
        {
            get { return Info.Stat; }
            set { Info.Stat = value; }
        }
        public PlayerInfo Info { get; set; }

        public string CharacterName
        {
            get
            {
                switch (Info.Character)
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

        public string icon = "●";
    }
}
