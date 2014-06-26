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

        static void entry()
        {
            Logger.enable();
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

    class TcpObserver : TcpServerObserver
    {
        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            
        }
    }
}
