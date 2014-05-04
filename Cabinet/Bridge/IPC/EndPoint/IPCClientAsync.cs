using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

using Cabinet.Utility;
using Cabinet.Bridge.IPC.RemoteObject;

namespace Cabinet.Bridge.IPC.EndPoint
{
    //warning: async mode is not under test
    public class IPCClientAsync : SingleListServer<IPCContext.RemoteMessage>
    {
        #region Private fields
        private IPCContext ipcContext { get; set; }

        private delegate void IPCClientEventHandler(object sender, IPCContext.RemoteMessage args);
        private event IPCClientEventHandler IPCClientEvent;
        #endregion

        #region Constructor
        public IPCClientAsync()
            : base(getContext().getResponseQueue(), getContext().getClientThreadEvent())
        {
            this.ipcContext = getContext();
        }

        public static IPCContext getContext()
        {
            return (IPCContext)Activator.GetObject(
                 typeof(IPCContext), IPCConfig.fullDescriptor);
        }
        #endregion

        #region Threading
        public override void start()
        {
            Logger.debug("IPCClientAsync: Staring...");
            base.start();
        }

        public override void stop()
        {
            Logger.debug("IPCClientAsync: Stopping...");
            base.stop();
        }
        #endregion

        #region Logical functions
        protected override void handleRequest(IPCContext.RemoteMessage request)
        {
            Logger.debug("onMessageResponse. msg = {0} ,param = {1}", request.descriptor, request.param);
        }
        

        public Guid postMessage(string message, string param)
        {
            try
            {
                RRemoteMessage msg = new RemoteMessage(false, business, method, param);

                ipcContext.postRequest(msg);

                return msg.guid;
            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: post with error: {0}.", ex.Message);
            }
        }

        public void registerIPCServerEventHandler(IPCServerEventHandler handler)
        {
            this.IPCServerEvent = handler;
        }



        #endregion
    }
}
