using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class ReportWiProcedureResultMessage : MessageBase
    {
        public ReportWiProcedureResultMessage(ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            verb = "reportWiProcedureResult";
            payload = reportWiProcedureResultTransactionVO.toJson();
        }
    }
}
