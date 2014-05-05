using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.IPC.EndPoint;
using Cabinet.Bridge.IPC.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Axis
{
    public class IPCAdaptor
    {
        
        private BusinessServer businessServer { get; set; }
        private IPCServer ipcServer { get; set; }

        public IPCAdaptor()
        {
            businessServer = new BusinessServer();
            ipcServer = new IPCServer();
            IPCServer.IPCServerEventHandler handler = new IPCServer.IPCServerEventHandler(this.onIPCMessage);
            ipcServer.registerIPCServerEventHandler(handler);
        }
        public void start()
        {
            Logger.debug("AxisServer: Launching servers...");
            businessServer.start();
            ipcServer.start();
        }

        public void stop()
        {
            Logger.debug("AxisServer: Closing servers...");
            businessServer.stop();
            ipcServer.stop();
        }

        private void onIPCMessage(object sender, IPCMessage message)
        {
            Logger.info("AxisServer: IPCServer - - -> BusinessServer.");
            Logger.debug("AxisServer: request = {0}.", message.request);
            BusinessRequest request = BusinessRequest.fromJson<BusinessRequest>(message.request);
            if(request == null)
            {
                Logger.error("AxisServer: corrupt message. skip.");
                return;
            }
            IPCBusinessResponse response = new IPCBusinessResponse(message);
            BusinessContext businessContext = new BusinessContext(request, response);
            businessServer.postRequest(businessContext);
            Logger.debug("AxisServer: IPCServer =====> BusinessServer.");
        }
    }
}
