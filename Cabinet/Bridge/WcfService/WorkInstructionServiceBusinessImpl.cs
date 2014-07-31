using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class WorkInstructionServiceBusinessImpl : ServiceBusinessImplBase
    {
        public WorkInstructionServiceBusinessImpl()
        {
            baseRequest.business = "workInstruction";
        }

        public string delivery(string wiDeliveryObject)
        {
            baseRequest.method = "delivery";
            Logger.debug("WcfServer: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, wiDeliveryObject);
            logOnPreparingRequest();

            WorkInstructionDeliveryVO vo = WorkInstructionDeliveryVO.fromJson<WorkInstructionDeliveryVO>(wiDeliveryObject);

            baseRequest.param.Add(vo);
            commitAndWait();
            if (baseResponse.isSuccess == false)
            {
                Logger.debug("WcfServer: business server returns error: {0}", baseResponse.errorMessage);
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
            Logger.debug("WcfServer: business server returns success.");
            logOnParsingResponse();
            WSResponseSuccessBase response = new WSResponseSuccessBase();
            return response.toJson();


        }
    }
}
