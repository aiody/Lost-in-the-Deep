using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class PacketHandler
{
    public static void S_EnterGameHandler(PacketSession session, IMessage packet)
    {
        S_EnterGame enterPacket = packet as S_EnterGame;
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = packet as S_Despawn;
    }

    public static void S_InitialCharacterInfoHandler(PacketSession session, IMessage packet)
    {
        S_InitialCharacterInfo infoPacket = packet as S_InitialCharacterInfo;
    }

    public static void S_UpdatePlayerInfoHandler(PacketSession session, IMessage packet)
    {
        S_UpdatePlayerInfo updatePacket = packet as S_UpdatePlayerInfo;
    }

    public static void S_ResRankingListHandler(PacketSession session, IMessage packet)
    {
        S_ResRankingList rankPacket = packet as S_ResRankingList;
    }
}
