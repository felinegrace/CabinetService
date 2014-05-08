using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.Ipc.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.Ipc.CommonEntity
{
    public class IpcBusinessResponse : BusinessResponse
    {
        private IpcMessage ipcMessageRef;
        public IpcBusinessResponse(IpcMessage ipcMessageRef)
        {
            this.ipcMessageRef = ipcMessageRef;
        }
        public override void onResponsed()
        {
            Logger.info("AxisServer: BusinessServer =====> IpcServer.");
            ipcMessageRef.response = base.toJson();
            Logger.debug("AxisServer: msg = {0}", ipcMessageRef.response);
            ipcMessageRef.notify();
            
        }
    }
}
