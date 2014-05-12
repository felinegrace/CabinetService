using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class RegionBusinessService : BusinessServiceBase
    {
        public RegionBusinessService()
        {
            baseRequest.business = "region";
        }

        public string create(string name, string shortName)
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
                Guid guid = (Guid)baseResponse.result.ElementAt<object>(0);
                WSRegionCreateResponse response = new WSRegionCreateResponse();
                response.regionGuid = guid.ToString();
                return response.toJson();
            }
            catch(Exception exception)
            {
                Logger.error("WcfServer: {0}.", exception.Message);
                return new WSResponseErrorBase(exception.Message).toJson();
            }


        }
    }
}
