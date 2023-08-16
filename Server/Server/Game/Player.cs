using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Google.Protobuf.Protocol.Action;

namespace Server
{
    internal class Player
    {
        public int Id
        {
            get { return Info.PlayerId; }
            set { Info.PlayerId = value; }
        }

        public GameRoom Room { get; set; }
        public ClientSession Session { get; set; }

        public Stat Stat
        {
            get { return Info.Stat; }
            set { Info.Stat = value; }
        }
        public PlayerInfo Info { get; set; } = new PlayerInfo();

        public static Stat InitStat(CharacterType character)
        {
            Stat stat = new Stat();
            stat.Depth = 5000;
            switch (character)
            {
                case CharacterType.Diver:

                    stat.Fuel = 300;
                    stat.Food = 100;
                    stat.Oxygen = 1500;
                    stat.Relic = 50;
                    break;
                case CharacterType.MarineBiologist:
                    stat.Fuel = 400;
                    stat.Food = 200;
                    stat.Oxygen = 1000;
                    stat.Relic = 50;
                    break;
                case CharacterType.Archaeologist:
                    stat.Fuel = 300;
                    stat.Food = 100;
                    stat.Oxygen = 1000;
                    stat.Relic = 100;
                    break;
            }
            return stat;
        }

        public void ApplyActionResult(Action targetAction)
        {
            Stat.Depth -= targetAction.Surge;
            Stat.Fuel += targetAction.Fuel;
            Stat.Food += targetAction.Food;
            Stat.Oxygen += targetAction.Oxygen;
            Stat.Relic += targetAction.Relic;

            if (Stat.Depth <= 0 || Stat.Fuel <= 0 || Stat.Food <= 0 || Stat.Oxygen <= 0)
            {
                if (Stat.Depth <= 0) // 게임 성공 시
                    Room.RankingBoard.WriteRecord(Info.Name, Stat.Relic);

                Room.LeaveGame(Id);
            }
            else
            {
                S_UpdatePlayerInfo updatePacket = new S_UpdatePlayerInfo();
                updatePacket.Player = Info;
                Room.Broadcast(updatePacket);
            }
        }
    }
}
