using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.BusinessLayer
{
    public class BusinessServer : SingleListServer<BusinessContext>
    {
        public BusinessServer() : base()
        {
            Logger.debug("BusinessServer: constructed.");
        }

        public override void start()
        {
            Logger.debug("BusinessServer: starting...");
            base.start();
        }

        public override void stop()
        {
            Logger.debug("BusinessServer: stopping...");
            base.stop();
        }

        protected override void onStart()
        {
            Logger.debug("BusinessServer: start.");
        }

        protected override void onStop()
        {
            Logger.debug("BusinessServer: stop.");
        }

        protected override void handleRequest(BusinessContext context)
        {
            Logger.debug("BusinessServer: handle request = {0}/{1} param = {2}",
                context.request.business, context.request.method,
                Logger.logObjectList(context.request.param));
            try
            {
                BOBase bo = BOFactory.getInstance(context);
                bo.handleBusiness();
                context.response.isSuccess = true;
            }
            catch(Exception Exception)
            {
                context.response.isSuccess = false;
                context.response.errorMessage = Exception.Message;
                Logger.error("BusinessServer: skip this request for reason: {0}", Exception.Message);
            }
            finally
            {
                context.response.onResponsed();
            }
        }


        
    }
}
