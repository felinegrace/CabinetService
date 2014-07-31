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
    public class EqptRoomClient : TcpEndPointObserver, MessageHandlerObserver
    {
        private TcpClient tcpClient { get; set; }
        private MessageHandler messageHandler { get; set; }
        public EqptRoomClient(string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            tcpClient = new TcpClient(clientIpAddress, clientPort,
                serverIpAddress, serverPort, this);
            messageHandler = new MessageHandler(this);
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

        public void register(Guid eqptRoomGuid)
        {
            Register registerEntity = new Register();
            registerEntity.eqptRoomGuid = eqptRoomGuid;
            RegisterMessage registerMessage = new RegisterMessage(registerEntity);
            tcpClient.send(registerMessage.rawMessage());
        }

        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        void MessageHandlerObserver.doAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            AcknowledgeMessage acknowledgeMessage = new AcknowledgeMessage(acknowledge);
            tcpClient.send(acknowledgeMessage.rawMessage());
        }

        void MessageHandlerObserver.onRegister(Guid sessionId, Register register)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageHandlerObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            Logger.debug("EqptRoomHubBusiness: server reports {0},{1}.",
                acknowledge.statusCode, acknowledge.message);
        }


        void MessageHandlerObserver.doDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        void MessageHandlerObserver.onDelivery(Guid sessionId, WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            UpdateWiStatusVO workInstructionReportVO1 = new UpdateWiStatusVO();
            workInstructionReportVO1.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO1.status = UpdateWiStatusVO.proceeding;
            doComplete(sessionId, workInstructionReportVO1);

            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                ReportWiProcedureResultVO workInstructionProcedureReportVO = new ReportWiProcedureResultVO();
                workInstructionProcedureReportVO.procedureGuid = workInstructionProcedureVO.procedureGuid;
                workInstructionProcedureReportVO.isSuccess = true;
                doReport(sessionId, workInstructionProcedureReportVO);
            }

            UpdateWiStatusVO workInstructionReportVO2 = new UpdateWiStatusVO();
            workInstructionReportVO2.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO2.status = UpdateWiStatusVO.complete;
            doComplete(sessionId, workInstructionReportVO2);
            /* 
             BusinessRequest completeRequest1 = new BusinessRequest();
            completeRequest1.business = "workInstruction";
            completeRequest1.method = "complete";
            completeRequest1.param.Add(workInstructionDeliveryVO.wiGuid);
            completeRequest1.param.Add("proceeding");
            BusinessResponse completeResponse1 = new testReportResponse();
            BusinessContext completeCtx1 = new BusinessContext(completeRequest1, completeResponse1);

            context.server.postRequest(completeCtx1);
            Logger.debug("BusinessServer: post workInstruction/complete with proceeding for test...");

            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                BusinessRequest request = new BusinessRequest();
                request.business = "workInstruction";
                request.method = "report";
                request.param.Add(workInstructionProcedureVO.procedureGuid);
                request.param.Add(true);
                BusinessResponse response = new testReportResponse();
                BusinessContext ctx = new BusinessContext(request, response);

                context.server.postRequest(ctx);
                Logger.debug("BusinessServer: delivery reporting pcd {0} for test...", workInstructionProcedureVO.procedureGuid);

            }

            BusinessRequest completeRequest2 = new BusinessRequest();
            completeRequest2.business = "workInstruction";
            completeRequest2.method = "complete";
            completeRequest2.param.Add(workInstructionDeliveryVO.wiGuid);
            completeRequest2.param.Add("complete");
            BusinessResponse completeResponse2 = new testReportResponse();
            BusinessContext completeCtx2 = new BusinessContext(completeRequest2, completeResponse2);

            context.server.postRequest(completeCtx2);
            Logger.debug("BusinessServer: post workInstruction/complete with complete for test...");
            Logger.debug("BusinessServer: business workInstruction/delivery ends.");
             */
        }

        public void doReport(Guid sessionId, ReportWiProcedureResultVO workInstructionProcedureReportVO)
        {
            WorkInstructionProcedureReportMessage workInstructionProcedureReportMessage = new WorkInstructionProcedureReportMessage(workInstructionProcedureReportVO);
            tcpClient.send(workInstructionProcedureReportMessage.rawMessage());
        }

        void MessageHandlerObserver.onReport(Guid sessionId, ReportWiProcedureResultVO workInstructionProcedureReportVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }

        public void doComplete(Guid sessionId, UpdateWiStatusVO workInstructionReportVO)
        {
            WorkInstructionReportMessage workInstructionReportMessage = new WorkInstructionReportMessage(workInstructionReportVO);
            tcpClient.send(workInstructionReportMessage.rawMessage());
        }

        void MessageHandlerObserver.onComplete(Guid sessionId, UpdateWiStatusVO workInstructionReportVO)
        {
            throw new EqptRoomCommException("client not supported.");
        }
    }
}
