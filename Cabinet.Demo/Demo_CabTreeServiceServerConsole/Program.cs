using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cabinet.Bridge.WcfService;

namespace Cabinet.Demo.CabTreeServiceServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WcfServer server = new WcfServer();
            server.start();
            while(true)
            {

            }
        }
    }
}
