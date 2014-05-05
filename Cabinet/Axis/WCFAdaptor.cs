using Cabinet.Bridge.WcfService;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Axis
{
    public class WcfAdaptor
    {
        
        private BusinessServer businessServer { get; set; }
        private WcfServer wcfServer { get; set; }

        public WcfAdaptor()
        {
            Logger.debug("AxisServer: constructing servers...");
            WcfServerGateway.getInstance().registerHanlder(
                new WcfServerGateway.WcfServiceGatewayEventHandler(
                this.onWCFMessage));
            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            
        }
        public void start()
        {
            Logger.debug("AxisServer: launching servers...");
            businessServer.start();
            wcfServer.start();
        }
        public void stop()
        {
            Logger.debug("AxisServer: closing servers...");
            businessServer.stop();
            wcfServer.stop();
        }

        private void onWCFMessage(object sender, WcfServerGatewayEventArgs args)
        {
            Logger.debug("AxisServer: WcfServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> BusinessServer.");
            Logger.debug("AxisServer: request = {0}/{1}. param = {2}", 
                args.context.request.business, args.context.request.method, 
                Logger.logObjectList(args.context.request.param));
            businessServer.postRequest(args.context);
            Logger.info("AxisServer: AxisServer =====> BusinessServer.");
        }

    }
}
