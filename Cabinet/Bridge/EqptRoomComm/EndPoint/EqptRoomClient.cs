using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomClient : EqptRoomClientMessageExchanger, TcpEndPointObserver
    {
        private EqptRoomClientObserver eqptRoomClientObserver { get; set; }
        private TcpClient tcpClient { get; set; }
        private MessageBusinessHandler messageHandler { get; set; }
        public EqptRoomClient(EqptRoomClientObserver eqptRoomClientObserver,
            string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            this.eqptRoomClientObserver = eqptRoomClientObserver;
            tcpClient = new TcpClient(clientIpAddress, clientPort,
                serverIpAddress, serverPort, this);
            messageHandler = new MessageBusinessHandler(this);
        }

        public void start()
        {
            Logger.debug("EqptRoomClient: starting...");
            tcpClient.start();
            Logger.debug("EqptRoomClient: start.");
        }

        public void stop()
        {
            Logger.debug("EqptRoomClient: stopping...");
            tcpClient.stop();
            Logger.debug("EqptRoomClient: stop.");
        }

        void TcpEndPointObserver.onTcpConnected(Guid sessionId)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubConnected();
            }
            
        }

        void TcpEndPointObserver.onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        void TcpEndPointObserver.onTcpDisconnected(Guid sessionId)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubDisconnected();
            }
        }

        void TcpEndPointObserver.onTcpError(Guid sessionId, string errorMessage)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onEqptRoomHubCommunicationError(errorMessage);
            }
        }

        protected override void onAcknowledgeMessage(Acknowledge acknowledge)
        {
            if (eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onAcknowledge(acknowledge);
            }
        }

        protected override void onDeliveryMessage(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            if(eqptRoomClientObserver != null)
            {
                eqptRoomClientObserver.onWorkInstrucionDelivery(workInstructionDeliveryVO);
            }
        }



        public override sealed Guid doRegister(Guid eqptRoomGuid)
        {
            Register registerEntity = new Register();
            registerEntity.eqptRoomGuid = eqptRoomGuid;
            RegisterMessage registerMessage = new RegisterMessage(registerEntity);
            tcpClient.send(registerMessage.rawMessage());
            return registerEntity.trasactionGuid;
        }

        public override sealed Guid doUpdateWiStatus(Guid eqptRoomGuid, UpdateWiStatusVO updateWiStatusVO)
        {
            UpdateWiStatusTransactionVO updateWiStatusTransactionVO = new UpdateWiStatusTransactionVO();
            updateWiStatusTransactionVO.eqptRoomGuid = eqptRoomGuid;
            updateWiStatusTransactionVO.updateWiStatusVO = updateWiStatusVO;
            UpdateWiStatusMessage workInstructionReportMessage = new UpdateWiStatusMessage(updateWiStatusTransactionVO);
            tcpClient.send(workInstructionReportMessage.rawMessage());
            return updateWiStatusTransactionVO.trasactionGuid;
        }

        public override sealed Guid doReportWiProcedureResult(Guid eqptRoomGuid, ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            ReportWiProcedureResultTransactionVO reportWiProcedureResultTransactionVO = new ReportWiProcedureResultTransactionVO();
            reportWiProcedureResultTransactionVO.eqptRoomGuid = eqptRoomGuid;
            reportWiProcedureResultTransactionVO.reportWiProcedureResultVO = reportWiProcedureResultVO;
            ReportWiProcedureResultMessage workInstructionProcedureReportMessage = new ReportWiProcedureResultMessage(reportWiProcedureResultTransactionVO);
            tcpClient.send(workInstructionProcedureReportMessage.rawMessage());
            return reportWiProcedureResultTransactionVO.trasactionGuid;
        }






    }
}
