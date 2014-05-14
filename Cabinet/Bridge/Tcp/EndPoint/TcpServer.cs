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
    public class TcpServer
    {
        private IocpSessionPool sessionPool { get; set; }
        private IocpListener listener { get; set; }
        

        private const int initialSessionPoolSize = 64;
        public TcpServer(string ipAddress, int port)
        {
            listener = new IocpListener(new IPEndPoint(IPAddress.Parse(ipAddress), port));
            listener.registerAcceptEventHanlder(this.onClientConnected);
            sessionPool = new IocpSessionPool(() => new IocpSession());
            
        }

        private void onClientConnected(object sender, IocpAcceptEventArgs args)
        {
            IocpSession newSession = sessionPool.take();
            
            newSession.attachSocket(args.socket);
            newSession.recv();
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
            sessionPool.dispose();
            Logger.debug("TcpServer: stop.");
        }
    }
}
