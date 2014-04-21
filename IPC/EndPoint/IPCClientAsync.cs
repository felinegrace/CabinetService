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
    public class IPCClientAsync
    {
        #region Private fields
        private IPCContext ipcContext = null;
        private Thread threadPostResponsePoll = null;
        private AutoResetEvent terminalPostResponsePollEvent = null;
        private class IPCPostResponsePollEventArgs : EventArgs
        {
            public IPCContext.RemoteMessage message { get; set; }
            public IPCPostResponsePollEventArgs(IPCContext.RemoteMessage aMessage)
                : base()
            {
                this.message = aMessage;
            }
        }
        private delegate void IPCPostResponsePollEventHandler(object sender, IPCPostResponsePollEventArgs args);
        private event IPCPostResponsePollEventHandler IPCPostResponsePollEvent;
        #endregion

        #region Constructor
        public IPCClientAsync()
        {
            ipcContext = (IPCContext)Activator.GetObject(
                typeof(IPCContext), IPCCommonContext.fullDescriptor);
            threadPostResponsePoll = new Thread(invokePostResponsePollEvent);
            terminalPostResponsePollEvent = new AutoResetEvent(false);
            IPCPostResponsePollEvent = new IPCPostResponsePollEventHandler(this.onMessageResponse);
        }
        #endregion

        #region Threading
        static void invokePostResponsePollEvent(object client)
        {
            IPCClientAsync transClient = client as IPCClientAsync;
            transClient.run();
        }

        void run()
        {
            Logger.debug("IPCServer: Post Response polling open.");
            while (!terminalPostResponsePollEvent.WaitOne(0))
            {
                peekPostResponse();
            }
            Logger.debug("IPCClient: Post Response polling Close.");
        }

        public void start()
        {
            threadPostResponsePoll.Start(this);
        }

        public void stop()
        {
            terminalPostResponsePollEvent.Set();
            ipcContext.getClientThreadEvent().Set();
        }
        #endregion

        #region Logical functions
        void peekPostResponse()
        {
            Logger.debug("IPCClient: Waiting for further response.");
            ipcContext.getClientThreadEvent().WaitOne(-1);
            while (ipcContext.getResponseQueueCount() > 0)
            {
                IPCContext.RemoteMessage msg = ipcContext.getResponseQueueFront();
                if (msg != null)
                {
                    IPCPostResponsePollEventArgs arg = new IPCPostResponsePollEventArgs(msg);
                    IPCPostResponsePollEvent(this, arg);
                }
                else
                {
                    Logger.error("IPCClient recv a null msg.");
                }
            }
            Logger.debug("IPCClient: responseQueue cleared.");
        }

        void onMessageResponse(object sender, IPCPostResponsePollEventArgs args)
        {
            Logger.debug("onMessageResponse. msg = {0} ,param = {1}",args.message.descriptor , args.message.param);
        }



        public void postMessage(string message, string param)
        {
            try
            {
                ipcContext.postMessage(message, param);
            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: post with error: {0}.", ex.Message);
            }
        }
        #endregion
    }
}
