using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class GameSession : Session
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConneced : {endPoint}");

            byte[] sendBuff = Encoding.UTF8.GetBytes("여기 서버 응답입니다.");
            Send(sendBuff);

            Thread.Sleep(1000);

            Disconnect();
        }

        public override void OnRecv(ArraySegment<byte> buffer)
        {
            string recvData = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
            Console.WriteLine($"클라에서 받은 메시지 : {recvData}");
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transffered bytes : {numOfBytes}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconneced : {endPoint}");
        }
    }

    internal class Program
    {
        static Listener _listener = new Listener();

        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(host);
            IPAddress ipAddr = entry.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.Init(endPoint, () => { return new GameSession(); });
            Console.WriteLine("LIstening...");

            while (true)
            {
            }
        }
    }
}