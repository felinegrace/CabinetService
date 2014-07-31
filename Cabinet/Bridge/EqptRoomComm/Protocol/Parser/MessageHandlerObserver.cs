using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    interface MessageHandlerObserver
    {
        
        void doAcknowledge(Guid sessionId, Acknowledge acknowledge);
        void onAcknowledge(Guid sessionId, Acknowledge acknowledge);
        void onRegister(Guid sessionId, Register register);
        void doDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO);
        void onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO);
        void doReport(Guid sessionId, ReportWiProcedureResultVO reportWiProcedureResultVO);
        void onReport(Guid sessionId, ReportWiProcedureResultVO reportWiProcedureResultVO);
        void doComplete(Guid sessionId, UpdateWiStatusVO updateWiStatusVO);
        void onComplete(Guid sessionId, UpdateWiStatusVO updateWiStatusVO);

    }
}
