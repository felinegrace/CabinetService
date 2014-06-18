using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Cabinet.Bridge.Tcp.Action;

namespace Cabinet.Bridge.Tcp.Session
{
    class IocpConnector
    {
        private Socket clientSocket { get; set; }
        private IPEndPoint clientEndPoint { get; set; }
        private IPEndPoint serverEndPoint { get; set; }
        private IocpConnectAction connectAction { get; set; }

        public IocpConnector(IPEndPoint clientEndPoint, IPEndPoint serverEndPoint, IIocpSessionObserver observer)
        {
            this.clientEndPoint = clientEndPoint;
            this.serverEndPoint = serverEndPoint;
            this.clientSocket = new Socket(clientEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            connectAction = new IocpConnectAction(clientSocket,
                ((socket) => { observer.onSessionConnected(socket); }));
        }

        public void start()
        {
            connectAction.connect(serverEndPoint);
        }

        public void stop()
        {
            connectAction.shutdown();
            clientSocket.Close();
        }
    }
}
