using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;
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
                case "reportWiProcedureResult":
                    doReportWiProcedureResult();
                    break;
                case "updateWiStatus":
                    doUpdateWiStatus();
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

            CommonModuleGateway.getInstance().eqptRoomCommModuleEntry.deliveryWorkInstrucion(workInstructionDeliveryVO);

        }

        void doReportWiProcedureResult()
        {
            Logger.debug("BusinessServer: business workInstruction/doReportWiProcedureResult starts.");
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(ReportWiProcedureResultVO));
            ReportWiProcedureResultVO reportWiProcedureResultVO = context.request.param.ElementAt<object>(0) as ReportWiProcedureResultVO;

            CommonModuleGateway.getInstance().wcfServiceModuleEntry.reportWiProcedureResult(reportWiProcedureResultVO);

            logOnFillingResult();
            Logger.debug("BusinessServer: reported procedure {0} as {1}...", reportWiProcedureResultVO.procedureGuid, reportWiProcedureResultVO.isSuccess);
            Logger.debug("BusinessServer: business workInstruction/doReportWiProcedureResult ends.");
        }

        void doUpdateWiStatus()
        {
            Logger.debug("BusinessServer: business workInstruction/updateWiStatus starts.");
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(UpdateWiStatusVO));

            UpdateWiStatusVO updateWiStatusVO = context.request.param.ElementAt<object>(0) as UpdateWiStatusVO;

            WcfServiceModuleEntry wcfServiceModuleEntry = CommonModuleGateway.getInstance().wcfServiceModuleEntry;

            switch (updateWiStatusVO.status)
            {
                case UpdateWiStatusVO.proceeding:
                    wcfServiceModuleEntry.updateWiStatusAsProceeding(updateWiStatusVO.workInstructionGuid);
                    break;
                case UpdateWiStatusVO.complete:
                    wcfServiceModuleEntry.updateWiStatusAsComplete(updateWiStatusVO.workInstructionGuid);
                    break;
                case UpdateWiStatusVO.fail:
                    wcfServiceModuleEntry.updateWiStatusAsFail(updateWiStatusVO.workInstructionGuid);
                    break;
            }

            logOnFillingResult();
            Logger.debug("BusinessServer: reported wi {0} as {1}...", updateWiStatusVO.workInstructionGuid, updateWiStatusVO.status);
            Logger.debug("BusinessServer: business workInstruction/updateWiStatus ends.");
        }
    }
}
