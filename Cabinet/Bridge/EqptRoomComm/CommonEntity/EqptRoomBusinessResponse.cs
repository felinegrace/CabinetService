using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm
{
    class EqptRoomBusinessResponse : BusinessResponse
    {
        public EqptRoomBusinessResponse()
        {
            
        }
        public override void onResponsed()
        {
            Logger.info("EqptRoomComm: business dealt.");
        }
    }
}
