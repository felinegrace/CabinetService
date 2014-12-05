using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.Tcp.EndPoint;

namespace Cabinet.Demo.ServerConsole
{
    class TcpServerConsole
    {

        public static void entry()
        {

            TcpServer s = new TcpServer("127.0.0.1", 8732, new TcpObserver());
            s.start();
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }
    }

    class TcpObserver : TcpEndPointObserver
    {
        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            
        }

        public void onTcpConnected(Guid sessionId)
        {
            
        }

        public void onTcpDisconnected(Guid sessionId)
        {
            
        }


        public void onTcpError(Guid sessionId, string errorMessage)
        {
            
        }
    }
}
