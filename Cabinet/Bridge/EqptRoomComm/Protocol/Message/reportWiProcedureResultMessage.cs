using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class ReportWiProcedureResultMessage : MessageBase
    {
        public ReportWiProcedureResultMessage(ReportWiProcedureResultVO workInstructionProcedureReportVO)
        {
            verb = "reportWiProcedureResult";
            payload = workInstructionProcedureReportVO.toJson();
        }
    }
}
