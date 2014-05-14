using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.Tcp.EndPoint;

namespace Demo_TcpClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
                    
            Logger.enable();
            TcpClient s = new TcpClient("127.0.0.1",6382,"127.0.0.1", 8732);
            s.start();
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }
    }
}
