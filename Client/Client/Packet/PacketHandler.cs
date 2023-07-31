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

        PlayerManager.Instance.Add(enterPacket.PlayerId, true);

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

        foreach (int id in spawnPacket.PlayerIds)
            PlayerManager.Instance.Add(id);
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
            myPlayer.Character = infoPacket.Character;
            myPlayer.Depth = infoPacket.Depth;
            myPlayer.Fuel = infoPacket.Fuel;
            myPlayer.Oxygen = infoPacket.Oxygen;
            myPlayer.Food = infoPacket.Food;
            myPlayer.Relic = infoPacket.Relic;
        }
    }
}
