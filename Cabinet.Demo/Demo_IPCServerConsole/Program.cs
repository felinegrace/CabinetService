using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.IPC.EndPoint;
using Cabinet.Utility;

namespace Cabinet.Demo.IPCServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            IPCServer s = new IPCServer();
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
