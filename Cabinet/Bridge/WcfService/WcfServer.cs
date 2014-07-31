using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;

namespace Cabinet.Bridge.WcfService
{
    public class WcfServer : WcfServiceModuleEntry
    {
        private ServiceHost serviceHost;

        private static int contractedWiStatusProceeding = 1;
        private static int contractedWiStatusComplete = 2;
        private static int contractedWiStatusFail = 3;
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



        public void reportWiProcedureResult(ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            string isSuccessString = reportWiProcedureResultVO.isSuccess ? "true" : "false";
            webComm.executeResultInfo(reportWiProcedureResultVO.procedureGuid.ToString(), isSuccessString);
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        private void reportWiStatus(Guid wiGuid, int wiStatus)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateWorkInstrStatus(wiGuid.ToString(), wiStatus);
            Logger.info("WcfServer: <3<3<3 Wcf Client Transaction Completed.");
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void updateWiStatusAsProceeding(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusProceeding);
        }

        public void updateWiStatusAsComplete(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusComplete);
        }

        public void updateWiStatusAsFail(Guid wiGuid)
        {
            reportWiStatus(wiGuid, contractedWiStatusFail);
        }
    }
}
