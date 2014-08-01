using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.WcfService;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.EndPoint;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;

namespace Cabinet.Axis
{
    public class ModuleLoader
    {
        private BusinessServer businessServer { get; set; }
        private WcfServer wcfServer { get; set; }
        private EqptRoomHub eqptRoomHub { get; set; }
        public ModuleLoader()
        {
            Logger.debug("ModuleLoader: loading config...");

            string eqptRoomHubListenIp = AppConfigHelper.GetValue("EqptRoomHubListenIp");
            string eqptRoomHubListenPortString = AppConfigHelper.GetValue("EqptRoomHubListenPort");
            int eqptRoomHubListenPort = Convert.ToInt32(eqptRoomHubListenPortString);

            Logger.debug("ModuleLoader: constructing servers...");

            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            eqptRoomHub = new EqptRoomHub(eqptRoomHubListenIp, eqptRoomHubListenPort);

            CommonModuleGateway.getInstance().businessServiceModuleEntry = businessServer;
            CommonModuleGateway.getInstance().wcfServiceModuleEntry = wcfServer;
            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry = eqptRoomHub;
        }
        public void start()
        {
            Logger.debug("ModuleLoader: launching servers...");
            businessServer.start();
            wcfServer.start();
            eqptRoomHub.start();
        }
        public void stop()
        {
            Logger.debug("ModuleLoader: closing servers...");
            businessServer.stop();
            wcfServer.stop();
            eqptRoomHub.stop();
        }

    }

}
