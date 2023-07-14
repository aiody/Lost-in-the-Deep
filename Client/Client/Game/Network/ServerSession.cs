using ServerCore;
using Google.Protobuf;
using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

internal class ServerSession : PacketSession
{
    public override void OnConnected(EndPoint endPoint)
    {
        // Console.WriteLine($"OnConneced : {endPoint}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        // Console.WriteLine($"OnDisconneced : {endPoint}");
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        PacketManager.Instance.OnRecvPacket(this, buffer);
    }

    public override void OnSend(int numOfBytes)
    {
        // Console.WriteLine($"Transffered bytes : {numOfBytes}");
    }
}
