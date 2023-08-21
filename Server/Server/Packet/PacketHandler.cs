using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerCore;
using Server;
using Google.Protobuf.Protocol;

internal class PacketHandler
{
    public static void C_SelectCharacterHandler(PacketSession session, IMessage packet)
    {
        C_SelectCharacter selectCharacterPacket = packet as C_SelectCharacter;
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null) return;

        GameRoom room = player.Room;
        if (room == null) return;

        room.Push(() => room.HandleSelectCharacter(player, selectCharacterPacket));
    }

    public static void C_SetPlayerNameHandler(PacketSession session, IMessage packet)
    {
        C_SetPlayerName namePacket = packet as C_SetPlayerName;
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null) return;

        GameRoom room = player.Room;
        if (room == null) return;

        room.Push(() => room.HandleSetPlayerName(player, namePacket));
    }

    public static void C_ChooseActionHandler(PacketSession session, IMessage packet)
    {
        C_ChooseAction chooseActionPacket = packet as C_ChooseAction;
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null) return;

        GameRoom room = player.Room;
        if (room == null) return;

        room.Push(() => room.HandleChooseAction(player, chooseActionPacket));
    }

    public static void C_RetryHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null) return;

        GameRoom room = RoomManager.Instance.GetRecentRoom();
        if (room == null)
            room = RoomManager.Instance.Add();

        room.Push(() => room.HandleRetry(player));
    }

    public static void C_ReqRankingListHandler(PacketSession session, IMessage packet)
    {
        ClientSession clientSession = session as ClientSession;

        Player player = clientSession.MyPlayer;
        if (player == null) return;

        GameRoom room = player.Room;
        if (room == null) return;

        room.Push(() => room.HandleReqRankingList(player));
    }
}
