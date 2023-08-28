using System.Net;
using ServerCore;
using Server;
using Google.Protobuf.Protocol;
using Google.Protobuf;

internal class ClientSession : PacketSession
{
    public Player MyPlayer { get; set; }
    public int SessionId { get; set; }

    public void Send(IMessage packet)
    {
        string msgName = packet.Descriptor.Name.Replace("_", string.Empty);
        MsgId msgId = (MsgId)Enum.Parse(typeof(MsgId), msgName);

        ushort size = (ushort)packet.CalculateSize();
        byte[] sendBuffer = new byte[size + 4];
        Array.Copy(BitConverter.GetBytes(size + 4), 0, sendBuffer, 0, sizeof(ushort));
        Array.Copy(BitConverter.GetBytes((ushort)msgId), 0, sendBuffer, 2, sizeof(ushort));
        Array.Copy(packet.ToByteArray(), 0, sendBuffer, 4, size);

        Send(new ArraySegment<byte>(sendBuffer));
    }

    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnConnected : {endPoint}");

        GameRoom room = RoomManager.Instance.GetRecentRoom();
        if (room == null)
            room = RoomManager.Instance.Add();

        MyPlayer = PlayerManager.Instance.Add(this);
        room.Push(() => room.EnterGame(MyPlayer));
    }

    public override void OnRecvPacket(ArraySegment<byte> buffer)
    {
        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);
        Console.WriteLine($"RecvPacketId : {id}, Size : {size}");

        PacketManager.Instance.OnRecvPacket(this, buffer);
    }

    public override void OnSend(int numOfBytes)
    {
        //Console.WriteLine($"Transffered bytes : {numOfBytes}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnDisconneced : {endPoint}");

        GameRoom room = MyPlayer.Room;
        if (room != null)
            room.Push(() => room.LeaveGame(MyPlayer.Id));

        PlayerManager.Instance.Remove(MyPlayer.Id);

        SessionManager.Instance.Remove(this);
    }
}
