using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using Cabinet.Utility;
using Cabinet.Bridge.Ipc.RemoteObject;
using Cabinet.Bridge.Ipc.CommonEntity;

namespace Cabinet.Bridge.Ipc.EndPoint
{
    public class IpcClientSync
    {
        #region Private fields
        private IpcContext ipcContext = null;
        #endregion

        #region Constructor
        public IpcClientSync()
        {
            ipcContext = (IpcContext)Activator.GetObject(
                typeof(IpcContext), IpcConfig.fullDescriptor);
        }
        #endregion

        #region Logical functions
        public string sendMessage(string request)
        {
            Logger.info("IpcClient: IpcClient - - -> IpcBridge.");
            Logger.debug("IpcClient: msg = {0}", request);
            try
            {

                IpcMessage msg = new IpcMessage(true, request);
                ipcContext.postRequest(msg);
                
                Logger.debug("IpcClient: waiting for response.", request);
                msg.wait();
                Logger.info("IpcClient: IpcClient =====> IpcBridge.");
                return msg.response;

            }
            catch (System.Exception ex)
            {
                Logger.error("IpcClient: send with error: {0}.", ex.Message);
                return null;
            }
        }
        #endregion


    }
}
