using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerCore;
using Server;
using Google.Protobuf.Protocol;
using Action = Google.Protobuf.Protocol.Action;

internal class PacketHandler
{
    public static void C_SelectCharacterHandler(PacketSession session, IMessage packet)
    {
        C_SelectCharacter selectCharacterPacket = packet as C_SelectCharacter;
        ClientSession clientSession = session as ClientSession;

        Player myPlayer = clientSession.MyPlayer;
        {
            myPlayer.type = selectCharacterPacket.Character;
            myPlayer.depth = 5000;
            myPlayer.fuel = 300;
            myPlayer.food = 100;
            myPlayer.oxygen = 1000;
            myPlayer.relic = 50;
        }

        S_InitialCharacterInfo initPacket = new S_InitialCharacterInfo()
        {
            Character = myPlayer.type,
            Depth = myPlayer.depth,
            Fuel = myPlayer.fuel,
            Food = myPlayer.food,
            Oxygen = myPlayer.oxygen,
            Relic = myPlayer.relic
        };

        clientSession.Send(initPacket);
    }

    public static void C_SetPlayerNameHandler(PacketSession session, IMessage packet)
    {
        C_SetPlayerName namePacket = packet as C_SetPlayerName;
        ClientSession clientSession = session as ClientSession;

        clientSession.MyPlayer.PlayerName = namePacket.Name;
    }

    public static void C_ChooseActionHandler(PacketSession session, IMessage packet)
    {
        C_ChooseAction chooseActionPacket = packet as C_ChooseAction;
        ClientSession clientSession = session as ClientSession;

        Event targetEvent = DataManager.Events.Find(e => e.Id == chooseActionPacket.EventId);
        
        if (targetEvent == null)
            return;

        Action targetAction = targetEvent.Actions.Where(a => a.Id == chooseActionPacket.ActionId).First();

        // TODO: 선택한 액션이 유효한지 검증

        clientSession.MyPlayer.ApplyActionResult(targetAction);
    }

    public static void C_RetryHandler(PacketSession session, IMessage packet)
    {
        C_Retry retryPacket = packet as C_Retry;
        ClientSession clientSession = session as ClientSession;

        GameRoom room = RoomManager.Instance.GetRecentRoom();
        if (room == null)
            room = RoomManager.Instance.Add();

        room.EnterGame(clientSession.MyPlayer);
    }
}
