using Google.Protobuf;
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
        Console.WriteLine(packet);
    }

    public static void S_LeaveGameHandler(PacketSession session, IMessage packet)
    {

    }
}
