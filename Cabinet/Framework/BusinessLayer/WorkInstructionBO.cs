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
                case "complete" :
                    doComplete();
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
            
            
            foreach(WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                Logger.debug("BusinessServer: delivery reporting pcd {0} for test...", workInstructionProcedureVO.procedureGuid);
                BusinessRequest request = new BusinessRequest();
                request.business = "workInstruction";
                request.method = "report";
                request.param.Add(workInstructionProcedureVO);
                BusinessResponse response = new testReportResponse();
                BusinessContext ctx = new BusinessContext(request,response);

                context.server.postRequest(ctx);
            }

            BusinessRequest completeRequest = new BusinessRequest();
            completeRequest.business = "workInstruction";
            completeRequest.method = "complete";
            completeRequest.param.Add(workInstructionDeliveryVO.wiGuid);
            completeRequest.param.Add(true);
            BusinessResponse completeResponse = new testReportResponse();
            BusinessContext completeCtx = new BusinessContext(completeRequest, completeResponse);

            context.server.postRequest(completeCtx);
        }

        class testReportResponse : BusinessResponse
        {
            public override void onResponsed()
            {
                
            }
        }

        void doReport()
        {
            logOnValidatingParams();
            validateParamCount(2);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid procedureGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(bool));
            bool isSuccess = (bool)context.request.param.ElementAt<object>(1);

            BusinessServerGateway businessServerGateway = BusinessServerGateway.getInstance();
            businessServerGateway.postWorkInstructionProcedureConfirmEvent(procedureGuid, isSuccess);
            logOnFillingResult();
            Logger.debug("BusinessServer: report procedure {0} as {1}...", procedureGuid, isSuccess);
            
        }

        void doComplete()
        {
            logOnValidatingParams();
            validateParamCount(2);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid wiGuid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(bool));
            bool isSuccess = (bool)context.request.param.ElementAt<object>(1);
            BusinessServerGateway businessServerGateway = BusinessServerGateway.getInstance();
            businessServerGateway.postWorkInstructionCompleteEvent(wiGuid, isSuccess);
            logOnFillingResult();
            Logger.debug("BusinessServer: report wi {0} as {1}...", wiGuid, isSuccess);
        }
    }
}
