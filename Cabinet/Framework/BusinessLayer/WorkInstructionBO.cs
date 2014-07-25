using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Framework.BusinessLayer
{
    class WorkInstructionBO : BOBase
    {
        public WorkInstructionBO(BusinessContext businessContext)
            : base(businessContext)
        {

        }

        public override void handleBusiness()
        {
            switch (context.request.method)
            {
                case "delivery":
                    doDelivery();
                    break;
                case "report":
                    doReport();
                    break;
                case "complete":
                    doComplete();
                    break;
                default:
                    break;
            }
        }

        void doDelivery()
        {
            Logger.debug("BusinessServer: business workInstruction/delivery starts.");
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(WorkInstructionDeliveryVO));
            WorkInstructionDeliveryVO workInstructionDeliveryVO = (WorkInstructionDeliveryVO)context.request.param.ElementAt<object>(0);
            BusinessServerGateway.getInstance().postWorkInstructionDeliveryEvent(workInstructionDeliveryVO);
            
        }


        //单步步骤报告
        void doReport()
        {
            Logger.debug("BusinessServer: business workInstruction/report starts.");
            logOnValidatingParams();
            validateParamCount(2);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid procedureGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(bool));
            bool isSuccess = (bool)context.request.param.ElementAt<object>(1);

            BusinessServerGateway businessServerGateway = BusinessServerGateway.getInstance();
            businessServerGateway.postWorkInstructionProcedureConfirmEvent(procedureGuid, isSuccess);
            logOnFillingResult();
            Logger.debug("BusinessServer: reported procedure {0} as {1}...", procedureGuid, isSuccess);
            Logger.debug("BusinessServer: business workInstruction/report ends.");
        }

        void doComplete()
        {
            Logger.debug("BusinessServer: business workInstruction/complete starts.");
            logOnValidatingParams();
            validateParamCount(2);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid wiGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(string));
            string status = context.request.param.ElementAt<object>(1) as string;
            BusinessServerGateway businessServerGateway = BusinessServerGateway.getInstance();
            switch (status)
            {
                case "proceeding":
                    businessServerGateway.postWorkInstructionProceedingEvent(wiGuid);
                    break;
                case "complete":
                    businessServerGateway.postWorkInstructionCompleteEvent(wiGuid);
                    break;
                case "fail":
                    businessServerGateway.postWorkInstructionFailEvent(wiGuid);
                    break;
            }

            logOnFillingResult();
            Logger.debug("BusinessServer: reported wi {0} as {1}...", wiGuid, status);
            Logger.debug("BusinessServer: business workInstruction/complete ends.");
        }
    }
}
