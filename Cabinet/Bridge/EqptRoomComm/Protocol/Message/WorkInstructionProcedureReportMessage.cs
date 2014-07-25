using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class WorkInstructionProcedureReportMessage : MessageBase
    {
        public WorkInstructionProcedureReportMessage(WorkInstructionProcedureReportVO workInstructionProcedureReportVO)
        {
            verb = "report";
            payload = workInstructionProcedureReportVO.toJson();
        }
    }
}
