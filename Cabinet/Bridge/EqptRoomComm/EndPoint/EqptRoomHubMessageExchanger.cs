
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public abstract class EqptRoomHubMessageExchanger : MessageObserver
    {
        void MessageObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        void MessageObserver.onRegister(Guid sessionId, Register register)
        {
            onRegisterMessage(sessionId, register);
        }

        void MessageObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            throw new EqptRoomCommException("server not supported.");
        }

        void MessageObserver.onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            onReportWiProcedureResult(sessionId, reportWiProcedureResultTransactionVO);
        }

        void MessageObserver.onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            onUpdateWiStatus(sessionId, updateWiStatusTransactionVO);
        }

        protected abstract void doDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO);

        protected abstract void onRegisterMessage(Guid sessionId, Register register);

        protected abstract void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO);

        protected abstract void onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO);
    }
}
