using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Ipc.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.Ipc.CommonEntity;

namespace Cabinet.Demo.ServerConsole
{
    class IpcServerConsole
    {
        public static void entry()
        {

            IpcServer s = new IpcServer();
            s.IpcServerEvent += (
                (sender, e) => IpcServerConsole.onMessage(sender, e));
            s.start();
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }

        static void onMessage(object sender, IpcMessage args)
        {
            args.notify();
        }
    }
}
