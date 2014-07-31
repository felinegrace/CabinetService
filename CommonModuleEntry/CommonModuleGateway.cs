using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.CommonModuleEntry
{
    public class CommonModuleGateway
    {
        private volatile static CommonModuleGateway instance = null;
        private static readonly object locker = new object();
        private CommonModuleGateway() { }
        public static CommonModuleGateway getInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        Logger.debug("CommonModuleGateway: constructing common module gateway...");
                        instance = new CommonModuleGateway();
                    }
                }
            }
            return instance;
        }

        public SingleListServerInterface<BusinessContext> businessServiceModuleEntry { get; set; }
        public WcfServiceModuleEntry wcfServiceModuleEntry { get; set; }
        public EqptRoomCommModuleEntry eqptRoomCommModuleEntry { get; set; }
    }
}
