﻿using System;
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
<<<<<<< HEAD
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            string isSuccessString = isSuccess ? "true" : "false";
            webComm.executeResultInfo(procedureGuid.ToString(), isSuccessString);
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void wiProceeding(Guid wiGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateWorkInstrStatus(wiGuid.ToString(), 1);
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void wiComplete(Guid wiGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateWorkInstrStatus(wiGuid.ToString(), 2);
            Logger.info("WcfServer: WcfServer =====> Webservice.");
        }

        public void wiFail(Guid wiGuid)
        {
            Logger.info("WcfServer: AxisServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> Webservice.");
            WebComm.WebServerService webComm = new WebComm.WebServerService();
            webComm.updateWorkInstrStatus(wiGuid.ToString(), 3);
            Logger.info("WcfServer: WcfServer =====> Webservice.");
=======
            WebServerClient webServerClient = new WebServerClient();
            webServerClient.updateWorkInstrStatus(procedureGuid.ToString(), isSuccess.ToString());
        }

        public void wiComplete(Guid wiGuid, bool isSuccess)
        {
            WebServerClient webServerClient = new WebServerClient();
            webServerClient.executeResultInfo(wiGuid.ToString(), isSuccess.ToString());
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
        }
    }
}
