using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.WcfService.CommonEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class RegionBusinessService : ServiceBusinessImplBase
    {
        public RegionBusinessService()
        {
            baseRequest.business = "region";
        }

        public string search(Guid guid)
        {
            baseRequest.method = "search";
            Logger.debug("WcfServer: comming request = {0}/{1} guid = {2}",
                baseRequest.business, baseRequest.method, guid);
            logOnPreparingRequest();
            baseRequest.param.Add(guid);
            commitAndWait();
            logOnParsingResponse();
            RegionVO vo = baseResponse.result.ElementAt<object>(0) as RegionVO;
            WSRegionSearchResponse response = new WSRegionSearchResponse();
            response.regionGuid = vo.guid.ToString();
            response.name = vo.name;
            response.shortName = vo.shortName;
            return response.toJson();
        }

        public string create(string name, string shortName)
        {
            baseRequest.method = "create";
            Logger.debug("WcfServer: comming request = {0}/{1} name = {2}, shortName = {3}",
                baseRequest.business, baseRequest.method, name, shortName);
            logOnPreparingRequest();
            baseRequest.param.Add(name);
            baseRequest.param.Add(shortName);
            commitAndWait();
            logOnParsingResponse();
            WSRegionCreateResponse response = new WSRegionCreateResponse();
            response.regionGuid = ((Guid)baseResponse.result.ElementAt<object>(0)).ToString();
            return response.toJson();
        }

        public string read()
        {
            baseRequest.method = "read";
            Logger.debug("WcfServer: comming request = {0}/{1}",
                baseRequest.business, baseRequest.method);
            logOnPreparingRequest();

            commitAndWait();
            logOnParsingResponse();
            WSRegionReadResponse response = new WSRegionReadResponse();
            response.regionGuidList = baseResponse.result.ElementAt<object>(0) as List<string>;
            return response.toJson();

        }

        public string update(Guid guid, string name, string shortName)
        {
            baseRequest.method = "update";
            Logger.debug("WcfServer: comming request = {0}/{1} guid = {2}, name = {3}, shortName = {4}",
                baseRequest.business, baseRequest.method, guid, name, shortName);
            logOnPreparingRequest();
            baseRequest.param.Add(guid.ToString());
            baseRequest.param.Add(name);
            baseRequest.param.Add(shortName);

            commitAndWait();
            logOnParsingResponse();
            WSRegionUpdateResponse response = new WSRegionUpdateResponse();
            return response.toJson();
            
        }

        public string delete(Guid guid)
        {
            baseRequest.method = "delete";
            Logger.debug("WcfServer: comming request = {0}/{1} guid = {2}",
                baseRequest.business, baseRequest.method, guid);
            logOnPreparingRequest();
            baseRequest.param.Add(guid.ToString());

            commitAndWait();
            logOnParsingResponse();
            WSRegionDeleteResponse response = new WSRegionDeleteResponse();
            return response.toJson();

        }
    }
}
