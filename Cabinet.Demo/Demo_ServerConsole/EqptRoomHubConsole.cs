using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.EndPoint;

namespace Cabinet.Demo.ServerConsole
{
    class EqptRoomHubConsole
    {
        public static void entry()
        {
            EqptRoomHub s = new EqptRoomHub("127.0.0.1", 8732);
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
