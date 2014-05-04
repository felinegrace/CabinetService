﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Axis;

namespace Cabinet.Demo.AxisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            AxisServer m = new AxisServer();

            System.Console.Write("press s\n");
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S:
                        m.start();
                        break;
                    case ConsoleKey.T:
                        m.stop();
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
        }
    }
}