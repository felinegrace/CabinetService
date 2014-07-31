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
        void onRegister(Guid sessionId, Register register);
        void onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO);
        void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultVO reportWiProcedureResultVO);
        void onUpdateWiStatus(Guid sessionId, UpdateWiStatusVO updateWiStatusVO);

    }
}
