using Google.Protobuf;
using Google.Protobuf.Protocol;
using Server.ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class GameRoom : IJobQueue
    {
        JobQueue _jobQueue = new JobQueue();
        object _lock = new object();

        public int RoomId { get; set; }
        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        public RankingBoard RankingBoard { get; private set; } = new RankingBoard();

        public void Push(System.Action job)
        {
            _jobQueue.Push(job);
        }

        public void EnterGame(Player player)
        {
            if (player == null)
                return;

            _players.Add(player.Id, player);
            player.Room = this;

            // 본인한테 정보 전송
            {
                S_EnterGame enterPacket = new S_EnterGame();
                enterPacket.Player = player.Info;
                foreach (Event e in DataManager.Events)
                    enterPacket.Events.Add(e);
                player.Session.Send(enterPacket);

                S_Spawn spawnPacket = new S_Spawn();
                foreach (Player p in _players.Values)
                {
                    if (p != player)
                        spawnPacket.Players.Add(p.Info);
                }
                player.Session.Send(spawnPacket);
            }

            // 타인한테 정보 전송
            {
                S_Spawn spawnPacket = new S_Spawn();
                spawnPacket.Players.Add(player.Info);
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

            _players.Remove(playerId);
            player.Room = null;

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
            lock (_lock)
            {
                return _players.Count >= 6;
            }
        }

        public void HandleSelectCharacter(Player player, C_SelectCharacter selectCharacterPacket)
        {
            player.Info.Character = selectCharacterPacket.Character;
            player.Info.Stat = Player.InitStat(selectCharacterPacket.Character);

            S_InitialCharacterInfo initPacket = new S_InitialCharacterInfo()
            {
                Character = player.Info.Character,
                Stat = player.Info.Stat
            };

            player.Session.Send(initPacket);
        }

        public void HandleSetPlayerName(Player player, C_SetPlayerName namePacket)
        {
            player.Info.Name = namePacket.Name;
        }

        public void HandleChooseAction(Player player, C_ChooseAction chooseActionPacket)
        {
            Event targetEvent = DataManager.Events.Find(e => e.Id == chooseActionPacket.EventId);

            if (targetEvent == null)
                return;

            Google.Protobuf.Protocol.Action targetAction = targetEvent.Actions.Where(a => a.Id == chooseActionPacket.ActionId).First();

            player.ApplyActionResult(targetAction);
        }

        public void HandleRetry(Player player)
        {
            player.InitPlayer();
            EnterGame(player);
        }

        public void HandleReqRankingList(Player player)
        {
            S_ResRankingList rankPacket = new S_ResRankingList();
            List<Record> ranks = RankingBoard.GetTop10Rank();
            
            foreach (Record rank in ranks)
                rankPacket.Ranks.Add(rank);

            player.Session.Send(rankPacket);
        }
    }
}
