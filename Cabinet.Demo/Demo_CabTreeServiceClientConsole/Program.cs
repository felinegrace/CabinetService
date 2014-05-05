using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo_CabTreeServiceClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CabTreeServiceClient c = new CabTreeServiceClient();
            int d = c.DoWork(4);
            System.Console.Write(d);
        }
    }
}
