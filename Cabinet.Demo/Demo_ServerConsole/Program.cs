using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Demo.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            EqptRoomHubConsole.entry();
        }
    }
}
