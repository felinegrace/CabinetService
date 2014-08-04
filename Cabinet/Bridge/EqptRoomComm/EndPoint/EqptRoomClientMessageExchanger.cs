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
    public abstract class EqptRoomClientMessageExchanger : MessageObserver
    {
        void MessageObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            onAcknowledgeMessage(acknowledge);
        }
        void MessageObserver.onRegister(Guid sessionId, Register register)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            onDeliveryMessage(workInstructionDeliveryVO);
        }

        void MessageObserver.onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onUpdateWiStatus(Guid sessionId, UpdateWiStatusTransactionVO updateWiStatusTransactionVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }
        public abstract Guid doRegister(Guid eqptRoomGuid);

        public abstract Guid doUpdateWiStatus(Guid eqptRoomGuid, UpdateWiStatusVO updateWiStatusVO);

        public abstract Guid doReportWiProcedureResult(Guid eqptRoomGuid, ReportWiProcedureResultVO reportWiProcedureResultVO);

        protected abstract void onAcknowledgeMessage(Acknowledge acknowledge);

        protected abstract void onDeliveryMessage(WorkInstructionDeliveryVO workInstructionDeliveryVO);



    }
}
