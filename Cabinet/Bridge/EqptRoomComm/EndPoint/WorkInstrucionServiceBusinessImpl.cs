using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class WorkInstrucionServiceBusinessImpl : EqptRoomHubBusinessBase
    {
        public WorkInstrucionServiceBusinessImpl()
        {
            baseRequest.business = "workInstruction";
        }

        public void reportWiProcedureResult(ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            baseRequest.method = "reportWiProcedureResult";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, reportWiProcedureResultVO.toJson());

            baseRequest.param.Add(reportWiProcedureResultVO);

            commit();


        }

        public void updateWiStatus(UpdateWiStatusVO updateWiStatusVO)
        {
            baseRequest.method = "updateWiStatus";
            Logger.debug("EqptRoomHub: comming request = {0}/{1} wiObj = {2}",
                baseRequest.business, baseRequest.method, updateWiStatusVO.toJson());

            baseRequest.param.Add(updateWiStatusVO);

            commit();


        }
    }
}
