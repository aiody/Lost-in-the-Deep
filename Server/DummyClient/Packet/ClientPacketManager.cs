using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;

internal class PacketManager
{
    #region Singleton
    static PacketManager _instance = new PacketManager();
    public static PacketManager Instance { get { return _instance; } }
    #endregion

    PacketManager()
    {
        Register();
    }

    Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
    Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();

    public void Register()
    {
        _onRecv.Add((ushort)MsgId.SEnterGame, MakePacket<S_EnterGame>);
        _handler.Add((ushort)MsgId.SEnterGame, PacketHandler.S_EnterGameHandler);
        _onRecv.Add((ushort)MsgId.SLeaveGame, MakePacket<S_LeaveGame>);
        _handler.Add((ushort)MsgId.SLeaveGame, PacketHandler.S_LeaveGameHandler);
        _onRecv.Add((ushort)MsgId.SSpawn, MakePacket<S_Spawn>);
        _handler.Add((ushort)MsgId.SSpawn, PacketHandler.S_SpawnHandler);
        _onRecv.Add((ushort)MsgId.SDespawn, MakePacket<S_Despawn>);
        _handler.Add((ushort)MsgId.SDespawn, PacketHandler.S_DespawnHandler);
        _onRecv.Add((ushort)MsgId.SInitialCharacterInfo, MakePacket<S_InitialCharacterInfo>);
        _handler.Add((ushort)MsgId.SInitialCharacterInfo, PacketHandler.S_InitialCharacterInfoHandler);
        _onRecv.Add((ushort)MsgId.SUpdatePlayerInfo, MakePacket<S_UpdatePlayerInfo>);
        _handler.Add((ushort)MsgId.SUpdatePlayerInfo, PacketHandler.S_UpdatePlayerInfoHandler);
        _onRecv.Add((ushort)MsgId.SResRankingList, MakePacket<S_ResRankingList>);
        _handler.Add((ushort)MsgId.SResRankingList, PacketHandler.S_ResRankingListHandler);
    }

    public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
    {
        ushort count = 0;

        ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
        count += 2;
        ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
        count += 2;

        Action<PacketSession, ArraySegment<byte>, ushort> action = null;
        if (_onRecv.TryGetValue(id, out action))
            action.Invoke(session, buffer, id);
    }

    void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
    {
        T pkt = new T();
        pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

        Action<PacketSession, IMessage> action = null;
        if (_handler.TryGetValue(id, out action))
            action.Invoke(session, pkt);
    }
}