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
            WcfServerGateway.getInstance().registerHanlder(
                new WcfServerGateway.WcfServiceGatewayEventHandler(
                this.onWCFMessage));
            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            
        }
        public void start()
        {
            Logger.debug("AxisServer: Launching servers...");
            businessServer.start();
            wcfServer.start();
        }
        public void stop()
        {
            Logger.debug("AxisServer: Closing servers...");
            businessServer.stop();
            wcfServer.stop();
        }

        private void onWCFMessage(object sender, WcfServerGatewayEventArgs args)
        {
            
            Logger.info("AxisServer: WCFServer - - -> BusinessServer.");
            Logger.debug("AxisServer: request = {0}/{1}.", args.context.request.business, args.context.request.method);
            businessServer.postRequest(args.context);
            Logger.debug("AxisServer: WCFServer =====> BusinessServer.");
        }

    }
}
