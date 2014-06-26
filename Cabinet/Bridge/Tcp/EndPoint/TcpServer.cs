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
    public class TcpServer : IIocpSessionObserver
    {
        private IocpSessionPool pendingSessions { get; set; }
        private IocpSessionMap runningSessions { get; set; }
        private IocpListener listener { get; set; }
        private TcpServerObserver tcpServerObserver { get; set; }

        private const int initialSessionPoolSize = 64;

        public TcpServer(string ipAddress, int port, TcpServerObserver tcpServerObserver)
        {
            this.tcpServerObserver = tcpServerObserver;
            listener = new IocpListener(new IPEndPoint(IPAddress.Parse(ipAddress), port), this);
            pendingSessions = new IocpSessionPool(() => new IocpSession(this));
            runningSessions = new IocpSessionMap();
        }

        public void start()
        {
            Logger.debug("TcpServer: starting...");
            listener.start();
            Logger.debug("TcpServer: start.");
        }

        public void stop()
        {
            Logger.debug("TcpServer: stopping...");
            listener.stop();
            Logger.debug("TcpServer: stop.");
        }

        public void onSessionConnected(Socket remoteSocket)
        {
            Logger.debug("TcpServer: client: {0}:{1} connected.",
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Address.ToString(),
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Port);
 	        IocpSession newSession = pendingSessions.take();
            Logger.debug("TcpServer: session {0} assigned to client: {1}:{2}",
                    newSession.sessionId,
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Address.ToString(),
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Port);
            runningSessions.put(newSession.sessionId, newSession);
            Logger.debug("TcpServer: attaching session {0} assigned to client: {1}:{2}",
                    newSession.sessionId,
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Address.ToString(),
                    (remoteSocket.RemoteEndPoint as IPEndPoint).Port);
            newSession.attachSocket(remoteSocket);
            newSession.recv();
        }

        public void onSessionData(Guid sessionId, Descriptor descriptor)
        {
            Logger.debug("TcpServer: session {0} receives {1} bytes of data. ascii data: {2}",
                    sessionId, descriptor.desLength, descriptor.toString(0, descriptor.desLength));
            Logger.debug("TcpServer: echo session {0} with {1} bytes of data. ascii data: {2}",
                    sessionId, descriptor.desLength, descriptor.toString(0, descriptor.desLength));
            runningSessions.search(sessionId).send(descriptor.des, 0, descriptor.desLength);
            if(tcpServerObserver != null)
            {
                tcpServerObserver.onTcpData(sessionId, descriptor);
            }
        }

        public void onSessionDisconnected(Guid sessionId)
        {
            IocpSession endSession = runningSessions.take(sessionId);
            pendingSessions.put(endSession);
        }

        public void sendData(Guid sessionId, byte[] data, int offset, int count)
        {
            IocpSession dstSession = runningSessions.search(sessionId);
            dstSession.send(data, offset, count);
        }
    }

}
