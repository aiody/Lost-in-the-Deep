using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using ServerCore;

namespace Client
{
    internal class NetworkManager
    {
        static NetworkManager _instance = new NetworkManager();
        public static NetworkManager Instance { get { return _instance; } }

        Connector _connector = new Connector();

        ServerSession _session = new ServerSession();

        bool _isLoading = false;

        public void Init()
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _connector.Init(endPoint, () => { return _session; });
        }

        public void Send(IMessage packet)
        {
            _session.Send(packet);
        }

        public void Loading()
        {
            _isLoading = true;
            while (_isLoading)
            {
                Console.Clear();
                Console.SetCursorPosition((Program.SCREEN_WIDTH / 2) - 4, Program.SCREEN_HEIGHT / 2);
                Console.Write("로딩중.");
                Thread.Sleep(250);
                Console.Write(".");
                Thread.Sleep(250);
                Console.Write(".");
                Thread.Sleep(250);
            }
        }

        public void ReleaseLoading()
        {
            _isLoading = false;
        }
    }
}
