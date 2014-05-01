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
    public class IPCServer : SingleListServer<IPCContext.RemoteMessage>
    {
        #region Private fields
        private IpcServerChannel channel;
        #endregion

        #region Constructor
        public IPCServer() : base(IPCContext.requestQueue, IPCContext.serverThreadEvent)
        {
            Logger.debug("IPCServer: Constructing...");
            channel = new IpcServerChannel(IPCConfig.channelDescriptor);
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
            Logger.debug("IPCServer: Open.");
        }

        protected override void onStop()
        {
            IPCClose();
            Logger.debug("IPCServer: Close.");
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

        #region Logical functions
        protected override void handleRequest(IPCContext.RemoteMessage request)
        {
            Logger.debug("IPCServer: handle request.");
            switch (request.type)
            {
                case IPCContext.RemoteMessage.MessageType.Synchronized:
                    onMessageSynchronized(request as IPCContext.RemoteMessageSynchronized);
                    break;
                case IPCContext.RemoteMessage.MessageType.Asynchronized:
                    onMessageAsynchronized(request as IPCContext.RemoteMessageAsynchronized);
                    break;
                default:
                    Logger.error("IPCServer: invalid request type.");
                    break;
            }
        }

        void onMessageSynchronized(IPCContext.RemoteMessageSynchronized message)
        {
            Logger.debug("msg = {0} arg = {1}", message.descriptor, message.param);
            onMessageSynchronizedComplete(message);
        }

        void onMessageSynchronizedComplete(IPCContext.RemoteMessageSynchronized message)
        {
            message.notifyEvent.Set();
        }

        void onMessageAsynchronized(IPCContext.RemoteMessageAsynchronized message)
        {
            Logger.debug("msg = {0} arg = {1}", message.descriptor, message.param);
            onMessageAsynchronizedComplete(message);
        }

        void onMessageAsynchronizedComplete(IPCContext.RemoteMessageAsynchronized message)
        {
            IPCContext.responseQueue.Enqueue(message);
            IPCContext.clientThreadEvent.Set();
        }





        #endregion
    }
}
