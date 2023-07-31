using System.Net;
using ServerCore;
using Server;

internal class ClientSession : PacketSession
{
    public Player MyPlayer { get; set; }

    public override void OnConnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnConnected : {endPoint}");

        GameRoom room = RoomManager.Instance.GetRecentRoom();
        if (room == null)
            room = RoomManager.Instance.Add();

        MyPlayer = PlayerManager.Instance.Add();
        MyPlayer.Session = this;
        room.EnterGame(MyPlayer);
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
        Console.WriteLine($"Transffered bytes : {numOfBytes}");
    }

    public override void OnDisconnected(EndPoint endPoint)
    {
        Console.WriteLine($"OnDisconneced : {endPoint}");

        GameRoom room = MyPlayer.Room;
        room.LeaveGame(MyPlayer.Id);

        PlayerManager.Instance.Remove(MyPlayer.Id);
    }
}
