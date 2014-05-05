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
            baseRequest.method = "create";
            baseRequest.param.Add(name);
            baseRequest.param.Add(shortName);
            try
            {
                commit();
                wait();
                return (int)(baseResponse.result.First<object>());
            }
            catch(Exception exception)
            {
                Logger.error("RegionBusinessService: {0}.", exception.Message);
                return default(int);
            }


        }
    }
}
