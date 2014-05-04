using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cabinet.Utility;

namespace Cabinet.Bridge.IPC.RemoteObject
{
    [Serializable]
    public class IPCMessage
    {
        public Guid guid { get; private set; }
        public string business { get; set; }
        public string method { get; set; }
        public string param { get; set; }
        public string result { get; set; }

        [NonSerialized]
        internal EventWaitHandle syncHandle;
        

        internal IPCMessage(bool isSync, string business, string method, string param)
        {
            this.guid = Guid.NewGuid();
            this.business = business;
            this.method = method;
            this.param = param;
            if (isSync)
            {
                syncHandle = new EventWaitHandle(false, EventResetMode.AutoReset, this.guid.ToString());
            }
            else
            {
                this.syncHandle = null;
            }
        }

        public void wait()
        {
            try
            {
                EventWaitHandle handle = EventWaitHandle.OpenExisting(this.guid.ToString());
                handle.WaitOne(-1);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Logger.debug("IPCMessage: async message should not wait.");
            }
        }

        public void notify()
        {
            try
            {
                EventWaitHandle handle = EventWaitHandle.OpenExisting(this.guid.ToString());
                handle.Set();
            }catch (WaitHandleCannotBeOpenedException)
            {
                Logger.debug("IPCMessage: async message should not notify.");
            }
        }
    }

    public class IPCMessageEventArgs : EventArgs
    {
        public IPCMessage message { get; set; }
        public IPCMessageEventArgs(IPCMessage message)
            : base()
        {
            this.message = message;
        }
    }
}
