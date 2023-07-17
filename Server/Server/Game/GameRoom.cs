using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class GameRoom
    {
        public int RoomId { get; set; }
        Dictionary<int, Player> _players = new Dictionary<int, Player>();

        public void EnterGame(Player player)
        {
            if (player == null)
                return;

            _players.Add(player.Id, player);
            player.Room = this;

            // 본인한테 정보 전송
            {
                S_EnterGame enterPacket = new S_EnterGame();
                enterPacket.PlayerId = player.Id;
                foreach (Event e in DataManager.Events)
                    enterPacket.Events.Add(e);
                player.Session.Send(enterPacket);

                S_Spawn spawnPacket = new S_Spawn();
                foreach (Player p in _players.Values)
                {
                    if (p != player)
                        spawnPacket.PlayerIds.Add(p.Id);
                }
                player.Session.Send(spawnPacket);
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.PlayerIds.Add(player.Id);
                foreach (Player p in _players.Values)
                {
                    if (p != player)
                        p.Session.Send(spawnPacket);
                }
            }
        }

        public void LeaveGame(int playerId)
        {
            Player player = null;
            if (_players.TryGetValue(playerId, out player) == false)
                return;

            // 본인한테 정보 전송
            {
                S_LeaveGame leavePacket = new S_LeaveGame();
                player.Session.Send(leavePacket);
            }

            // 타인한테 정보 전송
            {
                S_Despawn despawnPacket = new S_Despawn();
                despawnPacket.PlayerIds.Add(playerId);
                foreach (Player p in _players.Values)
                {
                    if (p != player)
                        p.Session.Send(despawnPacket);
                }
            }
        }

        public void Broadcast(IMessage packet)
        {
            foreach (Player p in _players.Values)
                p.Session.Send(packet);
        }

        public bool IsFull()
        {
            return _players.Count >= 6;
        }
    }
}
