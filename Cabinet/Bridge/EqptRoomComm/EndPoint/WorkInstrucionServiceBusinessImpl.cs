using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class WorkInstrucionServiceBusinessImpl : EqptRoomHubBusinessBase
    {
        public WorkInstrucionServiceBusinessImpl()
        {
            baseRequest.business = "workInstruction";
        }

        public void reportWiProcedureResult(ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            baseRequest.method = "reportWiProcedureResult";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, reportWiProcedureResultTransactionVO.toJson());

            baseRequest.param.Add(reportWiProcedureResultTransactionVO.trasactionGuid);
            baseRequest.param.Add(reportWiProcedureResultTransactionVO.eqptRoomGuid);
            baseRequest.param.Add(reportWiProcedureResultTransactionVO.reportWiProcedureResultVO);

            commit();


        }

        public void updateWiStatus(UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            baseRequest.method = "updateWiStatus";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, updateWiStatusTransactionVO.toJson());

            baseRequest.param.Add(updateWiStatusTransactionVO.trasactionGuid);
            baseRequest.param.Add(updateWiStatusTransactionVO.eqptRoomGuid);
            baseRequest.param.Add(updateWiStatusTransactionVO.updateWiStatusVO);

            commit();


        }
    }
}
