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

            EqptRoomClient s = new EqptRoomClient("127.0.0.1", 6382, "10.31.31.31", 8135);
            s.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S: s.register(new Guid("C9FB1218-5CB6-461D-A7C1-C23AF3EBEEDD"));
                        break;
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }
    }
}
