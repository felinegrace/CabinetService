using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService
{
    class RegionBusinessService : BusinessServiceBase
    {
        public RegionBusinessService()
        {
            baseRequest.business = "region";
        }

        public int create(string name, string shortName)
        {
            Logger.debug("WcfServer: comming request = {0}/{1} name = {2}, shortName = {3}",
                "region", "create", name, shortName);
            logOnPreparingRequest();
            baseRequest.method = "create";
            baseRequest.param.Add(name);
            baseRequest.param.Add(shortName);
            try
            {
                commitAndWait();
                logOnParsingResponse();
                int result = (int)(baseResponse.result.First<object>());
                return result;
            }
            catch(Exception exception)
            {
                Logger.error("WcfServer: {0}.", exception.Message);
                return default(int);
            }


        }
    }
}
