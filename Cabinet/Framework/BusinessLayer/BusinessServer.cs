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
            Logger.debug("BusinessManager: Constructed...");
        }

        public override void start()
        {
            Logger.debug("BusinessManager: Starting...");
            base.start();
        }

        public override void stop()
        {
            Logger.debug("BusinessManager: Stopping...");
            base.stop();
        }

        protected override void onStart()
        {
            Logger.debug("BusinessManager: Start.");
        }

        protected override void onStop()
        {
            Logger.debug("BusinessManager: Stop.");
        }

        protected override void handleRequest(BusinessContext context)
        {
            try
            {
                BOBase bo = BOFactory.getInstance(context);
                bo.handleBusiness();
            }
            catch(Exception Exception)
            {
                Logger.error("skip this request for reason: {0}",Exception.Message);
            }
            finally
            {
                context.response.onResponsed();
            }
        }


        
    }
}
