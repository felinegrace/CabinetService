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
    public class IPCServer : SingleListServer<IPCMessage>
    {
        #region Private fields
        private IpcServerChannel channel;

        public delegate void IPCServerEventHandler(object sender, IPCMessage args);
        private event IPCServerEventHandler IPCServerEvent;

        #endregion

        #region Constructor
        public IPCServer() : base(IPCContext.requestQueue, IPCContext.serverThreadEvent)
        {
            channel = new IpcServerChannel(IPCConfig.channelDescriptor);
            Logger.debug("IPCServer: Constructed...");
        }
        #endregion

        #region Threading
        public override void start()
        {
            Logger.debug("IPCServer: Starting...");
            base.start();
        }

        public override void stop()
        {
            Logger.debug("IPCServer: Stopping...");
            base.stop();
        }

        protected override void onStart()
        {
            IPCOpen();
            Logger.debug("IPCServer: Start.");
        }

        protected override void onStop()
        {
            IPCClose();
            Logger.debug("IPCServer: Stop.");
        }

        void IPCOpen()
        {
            try
            {
                ChannelServices.RegisterChannel(channel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(IPCContext),
                    IPCConfig.objectDescriptor,
                    WellKnownObjectMode.Singleton);
            }
            catch (System.Exception ex)
            {
                Logger.error("IPCServer: open with error: {0}.", ex.Message);
            }
        }

        void IPCClose()
        {
            channel.StopListening(null);
            ChannelServices.UnregisterChannel(channel);
        }
        #endregion

        public void registerIPCServerEventHandler(IPCServerEventHandler handler)
        {
            this.IPCServerEvent = handler;
        }

        #region Logical functions
        protected override void handleRequest(IPCMessage message)
        {
            Logger.info("IPCServer: IPCBridge =====> IPCServer.");
            Logger.debug("IPCServer: msg = {0}", message.request);
            IPCServerEvent(this, message);
        }

        public void postResponse(IPCMessage response)
        {
            Logger.info("IPCServer: IPCServer - - -> IPCBridge.");
            IPCContext.responseQueue.Enqueue(response);
            IPCContext.clientThreadEvent.Set();
            Logger.info("IPCServer: IPCServer =====> IPCBridge.");
        }

        #endregion
    }
}
