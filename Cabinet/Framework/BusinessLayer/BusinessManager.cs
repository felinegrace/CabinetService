using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.BusinessLayer
{
    public class BusinessManager
    {
        private Thread thread;
        private AutoResetEvent terminalEvent;
        private AutoResetEvent notifyEvent;

        public BusinessManager()
        {
            terminalEvent = new AutoResetEvent(false);
            notifyEvent = new AutoResetEvent(false);
        }

        public void start()
        {
            Logger.debug("BusinessManager: Staring...");
            thread = new Thread(invokeManger);
            thread.Start(this);
        }

        public void stop()
        {
            Logger.debug("BusinessManager: Stopping...");
            terminalEvent.Set();
            notifyEvent.Set();
        }

        void run()
        {
            IPCOpen();
            Logger.debug("IPCServer: Open.");
            while (!terminalEvent.WaitOne(0))
            {
                IPCPeekMessage();
            }
            IPCClose();
            Logger.debug("IPCServer: Close.");
        }

        static void invokeManager(object manager)
        {
            BusinessManager transManager = manager as BusinessManager;
            transManager.run();
        }
    }
}
