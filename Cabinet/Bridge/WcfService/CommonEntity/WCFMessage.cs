using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    [Serializable]
    public class WcfMessage
    {
        public Guid guid { get; private set; }

        [NonSerialized]
        internal EventWaitHandle syncHandle;

        internal WcfMessage(bool isSync)
        {
            this.guid = Guid.NewGuid();
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
                Logger.debug("WCFMessage: async message should not wait.");
            }
        }

        public void notify()
        {
            try
            {
                Logger.info("IPCServer: WCFServer - - -> WebService.");
                EventWaitHandle handle = EventWaitHandle.OpenExisting(this.guid.ToString());
                handle.Set();
                Logger.info("IPCServer: WCFServer =====> WebService.");
            }catch (WaitHandleCannotBeOpenedException)
            {
                Logger.debug("IPCServer: async message should not notify.");
            }
        }
    }

}
