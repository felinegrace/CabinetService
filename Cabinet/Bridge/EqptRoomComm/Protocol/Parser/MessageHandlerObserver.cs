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
        void doReport(Guid sessionId, WorkInstructionProcedureReportVO workInstructionProcedureReportVO);
        void onReport(Guid sessionId, WorkInstructionProcedureReportVO workInstructionProcedureReportVO);
        void doComplete(Guid sessionId, WorkInstructionReportVO workInstructionReportVO);
        void onComplete(Guid sessionId, WorkInstructionReportVO workInstructionReportVO);

    }
}
