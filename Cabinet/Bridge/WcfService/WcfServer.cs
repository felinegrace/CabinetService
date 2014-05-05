using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;

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
            Logger.debug("WcfServer: starting...");
            try
            {
                serviceHost.Open();
            }
            catch (System.Exception ex)
            {
                Logger.debug("WcfServer: error on start. {0}.", ex.Message);
            }
            Logger.debug("WcfServer: start.");
        }

        public void stop()
        {
            Logger.debug("WcfServer: stopping...");
            try
            {
                serviceHost.Close();
            }
            catch (System.Exception ex)
            {
                Logger.debug("WcfServer: error on start. {0}.", ex.Message);
            }
            Logger.debug("WcfServer: stop.");
            
        }

    }
}
