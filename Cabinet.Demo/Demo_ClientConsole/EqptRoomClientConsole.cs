using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.EndPoint;

namespace Cabinet.Demo.ClientConsole
{
    class EqptRoomClientConsole
    {
        public static void entry()
        {

            EqptRoomClient s = new EqptRoomClient("127.0.0.1", 6382, "127.0.0.1", 8732);
            s.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S: s.register(new Guid("0A3065ED-28F2-4F75-8A35-58333FF2E78B"));
                        break;
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }
    }
}
