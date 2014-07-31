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
    public class WcfAdaptor
    {
        private BusinessServer businessServer { get; set; }
        private WcfServer wcfServer { get; set; }
        private EqptRoomHub eqptRoomHub { get; set; }
        public WcfAdaptor()
        {
            Logger.debug("AxisServer: constructing servers...");

            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            eqptRoomHub = new EqptRoomHub("10.31.31.31", 8135);

            CommonModuleGateway.getInstance().businessServiceModuleEntry = businessServer;
            CommonModuleGateway.getInstance().wcfServiceModuleEntry = wcfServer;
            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry = eqptRoomHub;
        }
        public void start()
        {
            Logger.debug("AxisServer: launching servers...");
            businessServer.start();
            wcfServer.start();
            eqptRoomHub.start();
        }
        public void stop()
        {
            Logger.debug("AxisServer: closing servers...");
            businessServer.stop();
            wcfServer.stop();
            eqptRoomHub.stop();
        }

    }

}
