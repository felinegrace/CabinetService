using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Cabinet.Utility;
using Cabinet.Bridge.Tcp.Session;
using Cabinet.Bridge.Tcp.Action;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public class TcpClient : IIocpSessionObserver
    {
        private IocpSession session { get; set; }
        private IocpConnector connector { get; set; }

        public TcpClient(string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            session = new IocpSession(this);
            connector = new IocpConnector(
                new IPEndPoint(IPAddress.Parse(clientIpAddress), clientPort),
                new IPEndPoint(IPAddress.Parse(serverIpAddress), serverPort),
                this);
        }

        public void start()
        {
            Logger.debug("TcpClient: starting...");
            connector.start();
            Logger.debug("TcpClient: start.");
        }

        public void stop()
        {
            Logger.debug("TcpClient: stopping...");
            session.dispose(this, EventArgs.Empty);
            connector.stop();
            Logger.debug("TcpClient: stop.");
        }

        public void send(string buffer)
        {
            session.send(System.Text.Encoding.Default.GetBytes(buffer), 0, buffer.Length);
        }

        public void send(byte[] buffer, int offset, int count)
        {
            session.send(buffer, offset, count);
        }

        public void onSessionConnected(Socket remoteSocket)
        {
            session.attachSocket(remoteSocket);
            session.recv();
        }

        public void onSessionData(Guid sessionId, Descriptor descriptor)
        {
            Logger.debug("TcpServer: session {0} receives {1} bytes of data. ascii data: {2}",
                    sessionId, descriptor.desLength, descriptor.toString(0, descriptor.desLength));
        }

        public void onSessionDisconnected(Guid sessionId)
        {
            Logger.debug("TcpServer: session {0} disconnected.",
                    sessionId);
        }
    }
}
