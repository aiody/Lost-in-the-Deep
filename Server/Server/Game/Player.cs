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

        public PlayerInfo Info { get; set; } = new PlayerInfo();

        public void ApplyActionResult(Action targetAction)
        {
            Info.Depth -= targetAction.Surge;
            Info.Fuel += targetAction.Fuel;
            Info.Food += targetAction.Food;
            Info.Oxygen += targetAction.Oxygen;
            Info.Relic += targetAction.Relic;

            if (Info.Depth <= 0 || Info.Fuel <= 0 || Info.Food <= 0 || Info.Oxygen <= 0)
            {
                if (Info.Depth <= 0) // 게임 성공 시
                    Room.RankingBoard.WriteRecord(Info.Name, Info.Relic);

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
