using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    interface MessageObserver
    {
        void onAcknowledge(Guid sessionId, Acknowledge acknowledge);
        void onRegister(Guid sessionId, Register register);
        void onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO);
        void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO);
        void onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO);

    }
}
