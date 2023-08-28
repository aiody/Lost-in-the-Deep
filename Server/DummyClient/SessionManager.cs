using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
    internal class SessionManager
    {
        public static SessionManager Instance { get; private set; } = new SessionManager();
        object _lock = new object();
        List<SeverSession> _sessions = new List<SeverSession>();

        public void SendAll()
        {
            lock (_lock)
            {
                foreach (SeverSession session in _sessions)
                {
                    C_SelectCharacter selectCharacterPacket = new C_SelectCharacter();
                    selectCharacterPacket.Character = CharacterType.Diver;
                    session.Send(selectCharacterPacket);

                    C_SetPlayerName namePacket = new C_SetPlayerName();
                    namePacket.Name = "더미클라";
                    session.Send(namePacket);

                    for (int i = 0; i < 10; i++)
                    {
                        C_ChooseAction chooseActionPacket = new C_ChooseAction();
                        chooseActionPacket.EventId = i + 1;
                        chooseActionPacket.ActionId = ((i + 1) * 100) + 1;
                        session.Send(chooseActionPacket);
                    }
                    C_Retry retryPacket = new C_Retry();
                    session.Send(retryPacket);

                    C_ReqRankingList reqRankingPacket = new C_ReqRankingList();
                    session.Send(reqRankingPacket);
                }
            }
        }

        public SeverSession Generate()
        {
            lock (_lock)
            {
                SeverSession session = new SeverSession();
                _sessions.Add(session);
                return session;
            }
        }
    }
}
