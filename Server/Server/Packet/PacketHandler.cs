﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerCore;

internal class PacketHandler
{
    public static void C_GetEventHandler(PacketSession session, IMessage packet)
    {
        Console.WriteLine(packet);
    }
}
