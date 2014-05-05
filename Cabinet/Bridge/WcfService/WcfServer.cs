using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    public class WcfServer
    {
        private ServiceHost serviceHost;
        public WcfServer()
        {
            serviceHost = new ServiceHost(typeof(CabTreeService));
        }
        public void start()
        {
            serviceHost.Open();
        }

        public void stop()
        {

        }

    }
}
