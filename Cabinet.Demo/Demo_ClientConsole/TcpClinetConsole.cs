using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.Tcp.EndPoint;

namespace Cabinet.Demo.ClientConsole
{
    class TcpClinetConsole
    {
        static void entry()
        {

            Logger.enable();
            TcpClient s = new TcpClient("127.0.0.1", 6382, "127.0.0.1", 8732);
            s.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S: s.send("123456");
                        break;
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }
    }
}
