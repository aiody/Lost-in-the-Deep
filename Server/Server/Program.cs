using System.Net;
using System.Net.Sockets;
using System.Text;
using Server.Session;
using ServerCore;

namespace Server
{
    internal class Program
    {
        static Listener _listener = new Listener();

        static void Main(string[] args)
        {
            DataManager.LoadData();

            string host = Dns.GetHostName();
            IPHostEntry entry = Dns.GetHostEntry(host);
            IPAddress ipAddr = entry.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Listening...");

            while (true)
            {
            }
        }
    }
}