using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    internal class RoomManager
    {
        public static RoomManager Instance { get; } = new RoomManager();

        object _lock = new object();
        int _roomId = 1;
        Dictionary<int, GameRoom> _rooms = new Dictionary<int, GameRoom>();
        
        GameRoom recentRoom = null;

        public GameRoom Add()
        {
            GameRoom room = new GameRoom();

            lock (_lock)
            {
                room.RoomId = _roomId;
                _rooms.Add(_roomId, room);
                _roomId++;
            }

            recentRoom = room;
            return room;
        }

        public bool Remove(int roomId)
        {
            lock (_lock)
            {
                if (recentRoom.RoomId == roomId)
                    recentRoom = null;
                return _rooms.Remove(roomId);
            }
        }

        public GameRoom Find(int roomId)
        {
            lock (_lock)
            {
                GameRoom room = null;
                _rooms.TryGetValue(roomId, out room);
                return room;
            }
        }

        public GameRoom GetRecentRoom()
        {
            if (recentRoom == null)
                return null;
            if (recentRoom.IsFull())
                return null;
            return recentRoom;
        }
    }
}
