using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.Session;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public class TcpServer
    {
        private IocpSessionPool sessionPool { get; set; }


        private const int initialSessionPoolSize = 64;
        public TcpServer()
        {
            sessionPool = new IocpSessionPool(() => new IocpSession());
        }

        private void recycleSession(IocpSession session)
        {

        }

        public void start()
        {
            Logger.debug("TcpServer: staring...");
        }

        public void stop()
        {
            Logger.debug("TcpServer: stopping...");
            sessionPool.dispose();
        }
    }
}
