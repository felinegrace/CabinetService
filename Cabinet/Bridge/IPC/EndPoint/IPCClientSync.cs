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
    public class IPCClientSync
    {
        #region Private fields
        private IPCContext ipcContext = null;
        #endregion

        #region Constructor
        public IPCClientSync()
        {
            ipcContext = (IPCContext)Activator.GetObject(
                typeof(IPCContext), IPCConfig.fullDescriptor);
        }
        #endregion

        #region Logical functions
        public string sendMessage(string request)
        {
            Logger.info("IPCClient: IPCClient - - -> IPCBridge.");
            Logger.debug("IPCClient: msg = {0}", request);
            try
            {

                IPCMessage msg = new IPCMessage(true, request);
                ipcContext.postRequest(msg);
                
                Logger.debug("IPCClient: waiting for response.", request);
                msg.wait();
                Logger.info("IPCClient: IPCClient =====> IPCBridge.");
                return msg.response;

            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: send with error: {0}.", ex.Message);
                return null;
            }
        }
        #endregion


    }
}
