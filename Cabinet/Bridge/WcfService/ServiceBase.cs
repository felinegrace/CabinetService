using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class ServiceBase
    {
        protected void logOnRequest()
        {
            Logger.info("WcfServer: Webservice =====> WcfServer.");
        }

        protected void logOnResponse()
        {
            Logger.info("WcfServer: WcfServer =====> Webservice.");
            Logger.info("WcfServer: <3<3<3 Wcf Server Transaction Completed.");
        }

        protected string service(Func<string> serviceFunction)
        {
            try
            {
                logOnRequest();
                string result = serviceFunction();
                logOnResponse();
                return result;
            }
            catch (System.Exception ex)
            {
                Logger.error("WcfService: ws error returned to client: {0}",ex.Message);
                return new WSResponseErrorBase(ex.Message).toJson();
            }
            
        }
    }
}
