using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;
using Cabinet.Bridge.WcfService.CommonEntity;


namespace Cabinet.Bridge.WcfService
{
    class ServiceBusinessImplBase
    {
        private WcfMessage baseMessage { get; set; }
        internal BusinessRequest baseRequest { get; set; }
        internal WcfBusinessResponse baseResponse { get; set; }
        private BusinessContext baseContext { get; set; }
        protected ServiceBusinessImplBase()
        {
            baseMessage = new WcfMessage(true);
            baseRequest = new BusinessRequest();
            baseResponse = new WcfBusinessResponse(baseMessage);
            baseContext = new BusinessContext(baseRequest, baseResponse);
        }

        private void commit()
        {
            CommonModuleGateway.getInstance().businessServiceModuleEntry.postRequest(baseContext);
        }

        private void wait()
        {
            baseMessage.wait();
        }

        protected void commitAndWait()
        {
            Logger.debug("WcfServer: committing request to business server...");
            commit();
            Logger.debug("WcfServer: waiting for business server response...");
            wait();
            Logger.debug("WcfServer: business server responsed.");
        }

        protected void logOnPreparingRequest()
        {
            Logger.debug("WcfServer: preparing request...");
        }

        protected void logOnParsingResponse()
        {
            Logger.debug("WcfServer: parsing response...");
        }


    }
}
