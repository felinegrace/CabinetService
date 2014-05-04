using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.IPC.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.IPC.CommonEntity;

namespace Cabinet.Demo.IPCServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            IPCServer s = new IPCServer();
            s.registerIPCServerEventHandler(new IPCServer.IPCServerEventHandler(onMessage));
            s.start();
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }

        static void onMessage(object sender, IPCMessage args)
        {
            args.notify();
        }
    }
}
