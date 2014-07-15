﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.WcfService.CommonEntity;

namespace Cabinet.Bridge.WcfService
{
    class WorkInstructionBusinessService : BusinessServiceBase
    {
        public WorkInstructionBusinessService()
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
            if(baseResponse.isSuccess == false)
            {
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
            logOnParsingResponse();
            WSResponseSuccessBase response = new WSResponseSuccessBase();
            return response.toJson();
        

        }
    }
}
