using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.Ipc.EndPoint;
using Cabinet.Bridge.Ipc.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Axis
{
    public class IpcAdaptor
    {
        
        private BusinessServer businessServer { get; set; }
        private IpcServer ipcServer { get; set; }

        public IpcAdaptor()
        {
            businessServer = new BusinessServer();
            ipcServer = new IpcServer();
            IpcServer.IpcServerEventHandler handler = new IpcServer.IpcServerEventHandler(this.onIpcMessage);
            ipcServer.registerIpcServerEventHandler(handler);
        }
        public void start()
        {
            Logger.debug("AxisServer: staring...");
            businessServer.start();
            ipcServer.start();
        }

        public void stop()
        {
            Logger.debug("AxisServer: stopping...");
            businessServer.stop();
            ipcServer.stop();
        }

        private void onIpcMessage(object sender, IpcMessage message)
        {
            Logger.info("AxisServer: IpcServer =====> AxisServer.");
            Logger.info("AxisServer: AxisServer - - -> BusinessServer.");
            Logger.debug("AxisServer: request = {0}.", message.request);
            BusinessRequest request = BusinessRequest.fromJson<BusinessRequest>(message.request);
            if(request == null)
            {
                Logger.error("AxisServer: corrupt message. skip.");
                return;
            }
            IpcBusinessResponse response = new IpcBusinessResponse(message);
            BusinessContext businessContext = new BusinessContext(request, response);
            businessServer.postRequest(businessContext);
            Logger.info("AxisServer: AxisServer =====> BusinessServer.");
        }
    }
}
