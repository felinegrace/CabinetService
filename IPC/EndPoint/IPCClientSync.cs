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
    public class IPCClientSync
    {
        #region Private fields
        private IPCContext ipcContext = null;
        #endregion

        #region Constructor
        public IPCClientSync()
        {
            ipcContext = (IPCContext)Activator.GetObject(
                typeof(IPCContext), IPCCommonContext.fullDescriptor);
        }
        #endregion

        #region Logical functions
        public void sendMessage(string message, string param)
        {
            try
            {
                ipcContext.sendMessage(message, param);
                Logger.debug("IPCClient: send complete. msg = {0} , param = {1}",message , param);
            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: send with error: {0}.", ex.Message);
            }
        }
        #endregion


    }
}
