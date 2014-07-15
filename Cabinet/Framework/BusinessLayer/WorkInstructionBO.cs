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

            BusinessRequest completeRequest1 = new BusinessRequest();
            completeRequest1.business = "workInstruction";
            completeRequest1.method = "complete";
            completeRequest1.param.Add(workInstructionDeliveryVO.wiGuid);
            completeRequest1.param.Add("proceeding");
            BusinessResponse completeResponse1 = new testReportResponse();
            BusinessContext completeCtx1 = new BusinessContext(completeRequest1, completeResponse1);

            context.server.postRequest(completeCtx1);
            Logger.debug("BusinessServer: post workInstruction/complete with proceeding for test...");

            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                BusinessRequest request = new BusinessRequest();
                request.business = "workInstruction";
                request.method = "report";
                request.param.Add(workInstructionProcedureVO.procedureGuid);
                request.param.Add(true);
                BusinessResponse response = new testReportResponse();
                BusinessContext ctx = new BusinessContext(request, response);

                context.server.postRequest(ctx);
                Logger.debug("BusinessServer: delivery reporting pcd {0} for test...", workInstructionProcedureVO.procedureGuid);

            }

            BusinessRequest completeRequest2 = new BusinessRequest();
            completeRequest2.business = "workInstruction";
            completeRequest2.method = "complete";
            completeRequest2.param.Add(workInstructionDeliveryVO.wiGuid);
            completeRequest2.param.Add("complete");
            BusinessResponse completeResponse2 = new testReportResponse();
            BusinessContext completeCtx2 = new BusinessContext(completeRequest2, completeResponse2);

            context.server.postRequest(completeCtx2);
            Logger.debug("BusinessServer: post workInstruction/complete with complete for test...");
            Logger.debug("BusinessServer: business workInstruction/delivery ends.");
        }

        class testReportResponse : BusinessResponse
        {
            public override void onResponsed()
            {

            }
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
