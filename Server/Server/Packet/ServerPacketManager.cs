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
        _onRecv.Add((ushort)MsgId.CSelectCharacter, MakePacket<C_SelectCharacter>);
        _handler.Add((ushort)MsgId.CSelectCharacter, PacketHandler.C_SelectCharacterHandler);
        _onRecv.Add((ushort)MsgId.CSetPlayerName, MakePacket<C_SetPlayerName>);
        _handler.Add((ushort)MsgId.CSetPlayerName, PacketHandler.C_SetPlayerNameHandler);
        _onRecv.Add((ushort)MsgId.CChooseAction, MakePacket<C_ChooseAction>);
        _handler.Add((ushort)MsgId.CChooseAction, PacketHandler.C_ChooseActionHandler);
        _onRecv.Add((ushort)MsgId.CRetry, MakePacket<C_Retry>);
        _handler.Add((ushort)MsgId.CRetry, PacketHandler.C_RetryHandler);
        _onRecv.Add((ushort)MsgId.CReqRankingList, MakePacket<C_ReqRankingList>);
        _handler.Add((ushort)MsgId.CReqRankingList, PacketHandler.C_ReqRankingListHandler);
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