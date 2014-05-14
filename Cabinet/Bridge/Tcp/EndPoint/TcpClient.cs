using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Cabinet.Bridge.Tcp.Session;
using Cabinet.Bridge.Tcp.Action;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public class TcpClient
    {
        private IocpSession session { get; set; }
        private IocpConnector connector { get; set; }

        public TcpClient(string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            session = new IocpSession();
            connector = new IocpConnector(
                new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort),
                new IPEndPoint(IPAddress.Parse(serverIpAddress), serverPort)
                );
            connector.registerAcceptEventHanlder(this.onServerConnected);
        }

        public void start()
        {
            connector.start();
        }

        public void stop()
        {
            session.dispose(this, EventArgs.Empty);
            connector.stop();
        }

        private void onServerConnected(object sender, IocpConnectEventArgs args)
        {
            session.attachSocket(args.socket);
            session.recv();
        }
    }
}
