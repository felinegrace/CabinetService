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
    public class IPCServer
    {
        #region Private fields
        private Thread thread;
        private AutoResetEvent terminalEvent;
        private IpcServerChannel channel;
        private class IPCEventArgs : EventArgs
        {
            public IPCContext.RemoteMessage message {get;set;}
            public IPCEventArgs( IPCContext.RemoteMessage msg ) : base()
            {
                this.message = msg;
            }
        }
        private delegate void IPCEventHandler( object sender, IPCEventArgs args );
        private event IPCEventHandler IPCEvent;
        #endregion

        #region Constructor
        public IPCServer()
        {
            Logger.debug("IPCServer: Constructing...");
            channel = new IpcServerChannel(IPCConfig.channelDescriptor);
            terminalEvent = new AutoResetEvent(false);
            IPCEvent = new IPCEventHandler(this.dispatchMessage);
        }
        #endregion

        #region Threading
        public void start()
        {
            Logger.debug("IPCServer: Staring...");
            thread = new Thread(invokeServer);
            thread.Start(this);
        }

        public void stop()
        {
            Logger.debug("IPCServer: Stopping...");
            terminalEvent.Set();
            IPCContext.serverThreadEvent.Set();
        }

        void run()
        {
            IPCOpen();
            Logger.debug("IPCServer: Open.");
            while (!terminalEvent.WaitOne(0))
            {
                IPCPeekMessage();
            }
            IPCClose();
            Logger.debug("IPCServer: Close.");
        }

        static void invokeServer(object server)
        {
            IPCServer transServer = server as IPCServer;
            transServer.run();
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
        void IPCPeekMessage()
        {
            Logger.debug("IPCServer: Waiting for further message.");
            IPCContext.serverThreadEvent.WaitOne(-1);
            while (IPCContext.requestQueue.Count > 0)
            {
                IPCContext.RemoteMessage msg = null;
                lock (IPCContext.requestQueueMutex)
                {
                    msg = IPCContext.requestQueue.Dequeue();
                }
                IPCEventArgs arg = new IPCEventArgs(msg);
                IPCEvent(this, arg);
            }
            Logger.debug("IPCServer: requestQueue cleared.");
            //RemoteObject.messageEvent.Reset();
        }



        void dispatchMessage(object sender, IPCEventArgs args)
        {
            IPCContext.RemoteMessage msg = args.message;
            switch (msg.type)
            {
                case IPCContext.RemoteMessage.MessageType.Synchronized:
                    onMessageSynchronized(msg as IPCContext.RemoteMessageSynchronized);
                    break;
                case IPCContext.RemoteMessage.MessageType.Asynchronized:
                    onMessageAsynchronized(msg as IPCContext.RemoteMessageAsynchronized);
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
            lock (IPCContext.responseQueueMutex)
            {
                IPCContext.responseQueue.Enqueue(message);
            }
            IPCContext.clientThreadEvent.Set();
        }
        #endregion
    }
}
