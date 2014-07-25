using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class WorkInstructionReportMessage : MessageBase
    {
        public WorkInstructionReportMessage(WorkInstructionReportVO workInstructionReportVO)
        {
            verb = "complete";
            payload = workInstructionReportVO.toJson();
        }
    }
}
