﻿using Client;
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
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {
        Console.WriteLine($"Leave Game : {packet}");
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
            myPlayer.Info.Depth = infoPacket.Depth;
            myPlayer.Info.Fuel = infoPacket.Fuel;
            myPlayer.Info.Oxygen = infoPacket.Oxygen;
            myPlayer.Info.Food = infoPacket.Food;
            myPlayer.Info.Relic = infoPacket.Relic;
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
}
