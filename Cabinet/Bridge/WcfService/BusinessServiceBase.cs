using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class BusinessServiceBase
    {
        private WcfMessage baseMessage { get; set; }
        internal BusinessRequest baseRequest { get; set; }
        internal WcfBusinessResponse baseResponse { get; set; }
        private BusinessContext baseContext { get; set; }
        protected BusinessServiceBase()
        {
            baseMessage = new WcfMessage(true);
            baseRequest = new BusinessRequest();
            baseResponse = new WcfBusinessResponse(baseMessage);
            baseContext = new BusinessContext(baseRequest, baseResponse);
        }

        protected void wait()
        {
            baseMessage.wait();
        }

        protected void commit()
        {
            WcfServerGateway.getInstance().postEvent(baseContext);
        }
    }
}
