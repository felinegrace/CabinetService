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

        void MessageObserver.onRegister(Guid sessionId, Register register)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            onDeliveryMessage(workInstructionDeliveryVO);
        }

        void MessageObserver.onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageObserver.onUpdateWiStatus(Guid sessionId, UpdateWiStatusVO updateWiStatusVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        public abstract void doUpdateWiStatus(UpdateWiStatusVO workInstructionReportVO);

        public abstract void doReportWiProcedureResult(ReportWiProcedureResultVO workInstructionProcedureReportVO);

        protected abstract void onDeliveryMessage(WorkInstructionDeliveryVO workInstructionDeliveryVO);
    }
}
