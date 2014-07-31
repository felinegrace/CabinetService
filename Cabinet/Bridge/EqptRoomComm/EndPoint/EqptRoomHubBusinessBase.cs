using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class EqptRoomHubBusinessBase
    {
        internal BusinessRequest baseRequest { get; set; }
        internal EqptRoomBusinessResponse baseResponse { get; set; }
        private BusinessContext baseContext { get; set; }
        protected EqptRoomHubBusinessBase()
        {
            baseRequest = new BusinessRequest();
            baseResponse = new EqptRoomBusinessResponse();
            baseContext = new BusinessContext(baseRequest, baseResponse);
        }

        protected void commit()
        {
            Logger.debug("EqptRoomComm: committing request to business server...");
            CommonModuleGateway.getInstance().businessServiceModuleEntry.postRequest(baseContext);
        }
    }
}
