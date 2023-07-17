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

        Player myPlayer = clientSession.MyPlayer;
        {
            myPlayer.type = selectCharacterPacket.Character;
            myPlayer.depth = 5000;
            myPlayer.fuel = 100;
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
}
