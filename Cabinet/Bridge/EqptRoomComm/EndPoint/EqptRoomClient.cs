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

        protected override void onDeliveryMessage(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            UpdateWiStatusVO workInstructionReportVO1 = new UpdateWiStatusVO();
            workInstructionReportVO1.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO1.status = UpdateWiStatusVO.proceeding;
            doUpdateWiStatus(workInstructionReportVO1);

            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                ReportWiProcedureResultVO workInstructionProcedureReportVO = new ReportWiProcedureResultVO();
                workInstructionProcedureReportVO.procedureGuid = workInstructionProcedureVO.procedureGuid;
                workInstructionProcedureReportVO.isSuccess = true;
                doReportWiProcedureResult(workInstructionProcedureReportVO);
            }

            UpdateWiStatusVO workInstructionReportVO2 = new UpdateWiStatusVO();
            workInstructionReportVO2.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO2.status = UpdateWiStatusVO.complete;
            doUpdateWiStatus(workInstructionReportVO2);
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



        public override sealed void doUpdateWiStatus(UpdateWiStatusVO workInstructionReportVO)
        {
            UpdateWiStatusMessage workInstructionReportMessage = new UpdateWiStatusMessage(workInstructionReportVO);
            tcpClient.send(workInstructionReportMessage.rawMessage());
        }

        public override sealed void doReportWiProcedureResult(ReportWiProcedureResultVO workInstructionProcedureReportVO)
        {
            ReportWiProcedureResultMessage workInstructionProcedureReportMessage = new ReportWiProcedureResultMessage(workInstructionProcedureReportVO);
            tcpClient.send(workInstructionProcedureReportMessage.rawMessage());
        }

    }
}
