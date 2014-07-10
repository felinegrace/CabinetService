using Cabinet.Framework.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService
{
    public class WcfServerGateway
    {
        private volatile static WcfServerGateway instance = null;
        private static readonly object locker = new object();
        private WcfServerGateway() { }
        public static WcfServerGateway getInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        Logger.debug("WcfServer: constructing WcfServer gateway...");
                        instance = new WcfServerGateway();
                    }
                }
            }
            return instance;
        }

        public delegate void WcfBusinessEventHandler(object sender, WcfBusinessEventArgs args);
        internal event WcfBusinessEventHandler WcfBusinessEvent;

        public void registerHanlder(WcfBusinessEventHandler handler)
        {
            WcfBusinessEvent = handler;
        }
        
        public void postWcfBusinessEvent(BusinessContext context)
        {
            Logger.debug("WcfServer: WcfServer - - -> AxisServer");
            WcfBusinessEvent(this, new WcfBusinessEventArgs(context));
        }
    }

    public class WcfBusinessEventArgs : EventArgs
    {
        public BusinessContext context { get; set; }
        public WcfBusinessEventArgs(BusinessContext context)
        {
            this.context = context;
        }
    }
}
