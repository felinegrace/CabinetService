using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cabinet.Utility
{
    public abstract class PollingThread
    {
        private Thread thread { get; set; }
        private AutoResetEvent terminalEvent { get; set; }
        public PollingThread()
        {
            terminalEvent = new AutoResetEvent(false);
        }

        static void invoke(object poller)
        {
            PollingThread transPoller = poller as PollingThread;
            transPoller.polling();
        }

        public virtual void start()
        {
            thread = new Thread(invoke);
            thread.Start(this);
        }

        public virtual void stop()
        {
            terminalEvent.Set();
        }

        protected virtual void polling()
        {
            while (!terminalEvent.WaitOne(0))
            {
                onPolling();
            }
        }

        protected abstract void onPolling();
    }
}
