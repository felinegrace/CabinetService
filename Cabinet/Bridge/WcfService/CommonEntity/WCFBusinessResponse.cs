using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    public class WcfBusinessResponse : BusinessResponse
    {
        private WcfMessage wcfMessageRef;
        public WcfBusinessResponse(WcfMessage wcfMessageRef)
        {
            this.wcfMessageRef = wcfMessageRef;
        }
        public override void onResponsed()
        {
            Logger.info("WcfServer: BusinessServer - - -> WcfServer.");
            wcfMessageRef.notify();
        }
    }
}
