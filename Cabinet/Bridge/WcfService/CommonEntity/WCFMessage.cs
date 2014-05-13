using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    [Serializable]
    class WcfMessage
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
                Logger.info("WcfServer: pausing WcfServer thread...");
                EventWaitHandle handle = EventWaitHandle.OpenExisting(this.guid.ToString());
                handle.WaitOne(-1);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                Logger.debug("WcfServer: async message should not wait.");
            }
        }

        public void notify()
        {
            Logger.info("WcfServer: BusinessServer =====> WcfServer.");
            Logger.info("WcfServer: WcfServer - - -> WebService.");
            Logger.info("WcfServer: resuming WcfServer thread...");
            try
            {
                EventWaitHandle handle = EventWaitHandle.OpenExisting(this.guid.ToString());
                handle.Set();
            }catch (WaitHandleCannotBeOpenedException)
            {
                Logger.debug("WcfServer: async message should not notify.");
            }
        }
    }

}
