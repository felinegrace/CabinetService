﻿using Cabinet.Framework.CommonEntity;
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

        public delegate void WcfServiceGatewayEventHandler(object sender, WcfServerGatewayEventArgs args);
        internal event WcfServiceGatewayEventHandler WcfServiceGatewayEvent;

        public void registerHanlder(WcfServiceGatewayEventHandler handler)
        {
            WcfServiceGatewayEvent = handler;
        }
        
        public void postEvent(BusinessContext context)
        {
            Logger.debug("WcfServer: WcfServer - - -> AxisServer");
            WcfServiceGatewayEvent(this, new WcfServerGatewayEventArgs(context));
        }
    }

    public class WcfServerGatewayEventArgs : EventArgs
    {
        public BusinessContext context { get; set; }
        public WcfServerGatewayEventArgs(BusinessContext context)
        {
            this.context = context;
        }
    }
}
