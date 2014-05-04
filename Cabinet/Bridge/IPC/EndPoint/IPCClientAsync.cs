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
using Cabinet.Bridge.IPC.CommonEntity;

namespace Cabinet.Bridge.IPC.EndPoint
{
    //warning: async mode is not under test
    public class IPCClientAsync : SingleListServer<IPCMessage>
    {
        #region Private fields
        private IPCContext ipcContext { get; set; }

        public delegate void IPCClientEventHandler(object sender, IPCMessage args);
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

        public Guid postMessage(string request)
        {
            try
            {
                IPCMessage msg = new IPCMessage(false, request);
                ipcContext.postRequest(msg);
                return msg.guid;
            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: post with error: {0}.", ex.Message);
                return Guid.Empty;
            }
        }

        protected override void handleRequest(IPCMessage message)
        {
            Logger.debug("onMessageResponse. msg = {0}",
                message.request);
            IPCClientEvent(this, message);
        }

        public void registerIPCClientEventHandler(IPCClientEventHandler handler)
        {
            this.IPCClientEvent = handler;
        }



        #endregion
    }
}
