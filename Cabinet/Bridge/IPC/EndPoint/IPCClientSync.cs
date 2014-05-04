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
                typeof(IPCContext), IPCConfig.fullDescriptor);
        }
        #endregion

        #region Logical functions
        public string sendMessage(string business, string method, string param)
        {
            try
            {
                RemoteMessage msg = new RemoteMessage(true, business, method, param);

                ipcContext.postRequest(msg);

                Logger.debug("IPCClient: send complete. waiting for response. msg = {0}/{1} , param = {2}", business, method, param);

                msg.notifyEvent.WaitOne(-1);

                return msg.result;

            }
            catch (System.Exception ex)
            {
                Logger.error("IPCClient: send with error: {0}.", ex.Message);
            }
        }
        #endregion


    }
}
