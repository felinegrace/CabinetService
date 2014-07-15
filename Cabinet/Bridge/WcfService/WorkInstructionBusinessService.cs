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
<<<<<<< HEAD
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
=======
<<<<<<< HEAD
                Logger.debug("WcfServer: business server returns error: {0}", baseResponse.errorMessage);
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
            Logger.debug("WcfServer: business server returns success.");
=======
                return new WSResponseErrorBase(baseResponse.errorMessage).toJson();
            }
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
            logOnParsingResponse();
            WSResponseSuccessBase response = new WSResponseSuccessBase();
            return response.toJson();
        

        }
    }
}
