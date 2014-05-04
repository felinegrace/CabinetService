using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.IPC.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Axis
{
    class IPCBusinessResponse : BusinessResponse
    {
        private IPCMessage ipcMessageRef;
        public IPCBusinessResponse(IPCMessage ipcMessageRef)
        {
            this.ipcMessageRef = ipcMessageRef;
        }
        public override void onResponsed()
        {
            Logger.info("AxisServer: BusinessServer =====> IPCServer.");
            ipcMessageRef.response = base.toJson();
            Logger.debug("AxisServer: msg = {0}", ipcMessageRef.response);
            ipcMessageRef.notify();
            
        }
    }
}
