using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    public class WcfServer
    {
        private ServiceHost serviceHost;
        public WcfServer()
        {
            serviceHost = new ServiceHost(typeof(WorkInstructionService));
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

        public void wiReportProcedure(Guid procedureGuid, bool isSuccess)
        {
            WebServerClient webServerClient = new WebServerClient();
            webServerClient.updateWorkInstrStatus(procedureGuid.ToString(), isSuccess.ToString());
        }

        public void wiComplete(Guid wiGuid, bool isSuccess)
        {
            WebServerClient webServerClient = new WebServerClient();
            webServerClient.executeResultInfo(wiGuid.ToString(), isSuccess.ToString());
        }
    }
}
