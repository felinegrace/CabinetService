using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService
{
    class ServiceImplBase
    {
        protected void logOnRequest()
        {
            Logger.info("WcfServer: Webservice =====> WcfServer.");
        }

        protected void logOnResponse()
        {
            Logger.info("WcfServer: WcfServer =====> Webservice.");
            Logger.info("WcfServer: <3<3<3 Transaction Completed.");
        }

        protected string service(Func<string> serviceFunction)
        {
            logOnRequest();
            string result = serviceFunction();
            logOnResponse();
            return result;
        }
    }
}
