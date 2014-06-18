using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.BusinessLayer
{
    class WorkInstructionBO : BOBase
    {
        public WorkInstructionBO(BusinessContext businessContext) : base(businessContext)
        {

        }

        public override void handleBusiness()
        {
            switch (context.request.method)
            {
                case "delivery" :
                    doDelivery();
                    break;
                case "report" :
                    doReport();
                    break;
                default:
                    break;
            }
        }

        void doDelivery()
        {
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(WorkInstructionDeliveryVO));
            WorkInstructionDeliveryVO workInstructionDeliveryVO = (WorkInstructionDeliveryVO)context.request.param.ElementAt<object>(0);
            
        }

        void doReport()
        {

        }
    }
}
