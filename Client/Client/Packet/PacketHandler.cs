using Client;
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

        PlayerManager.Instance.Add(enterPacket.Player, true);

        foreach (Event e in enterPacket.Events)
            DataManager.Instance.Events.Add(e);

        NetworkManager.Instance.ReleaseLoading();
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        PlayerManager.Instance.Clear();
    }

    public static void S_SpawnHandler(PacketSession session, IMessage packet)
    {
        S_Spawn spawnPacket = packet as S_Spawn;

        foreach (PlayerInfo info in spawnPacket.Players)
            PlayerManager.Instance.Add(info);
    }

    public static void S_DespawnHandler(PacketSession session, IMessage packet)
    {
        S_Despawn despawnPacket = packet as S_Despawn;

        foreach (int id in despawnPacket.PlayerIds)
            PlayerManager.Instance.Remove(id);
    }

    public static void S_InitialCharacterInfoHandler(PacketSession session, IMessage packet)
    {
        S_InitialCharacterInfo infoPacket = packet as S_InitialCharacterInfo;
        Player myPlayer = PlayerManager.Instance.MyPlayer;
        {
            myPlayer.Info.Character = infoPacket.Character;
            myPlayer.Info.Stat = infoPacket.Stat;
        }
    }

    public static void S_UpdatePlayerInfoHandler(PacketSession session, IMessage packet)
    {
        S_UpdatePlayerInfo updatePacket = packet as S_UpdatePlayerInfo;

        Player player = PlayerManager.Instance.Find(updatePacket.Player.PlayerId);
        if (player != null)
        {
            player.Info = updatePacket.Player;
        }
    }

    public static void S_ResRankingListHandler(PacketSession session, IMessage packet)
    {
        S_ResRankingList rankPacket = packet as S_ResRankingList;

        DataManager.Instance.Ranking.Clear();

        foreach (Record rank in rankPacket.Ranks)
            DataManager.Instance.Ranking.Add(rank);

        NetworkManager.Instance.ReleaseLoading();
    }
}
