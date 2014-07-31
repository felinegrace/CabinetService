using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class UpdateWiStatusMessage : MessageBase
    {
        public UpdateWiStatusMessage(UpdateWiStatusVO workInstructionReportVO)
        {
            verb = "updateWiStatus";
            payload = workInstructionReportVO.toJson();
        }
    }
}
