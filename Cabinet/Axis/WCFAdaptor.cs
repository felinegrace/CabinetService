
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.WcfService;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.EndPoint;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Axis
{
    public class WcfAdaptor
    {

        private BusinessServer businessServer { get; set; }
        private WcfServer wcfServer { get; set; }
        private EqptRoomHub eqptRoomHub { get; set; }
        public WcfAdaptor()
        {
            Logger.debug("AxisServer: constructing servers...");
            WcfServerGateway.getInstance().registerHanlder(
                new WcfServerGateway.WcfBusinessEventHandler(
                this.onWCFMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionProcedureConfirmEventHandler(
                this.onWorkInstructionProcedureConfirmMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionProceedingEventHandler(
                this.onWorkInstructionProceedingMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionCompleteEventHandler(
                this.onWorkInstructionCompleteMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionFailEventHandler(
                this.onWorkInstructionFailMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionDeliveryEventHandler(
                    this.onWorkInstructionDelivery)
                );
            EqptRoomHubGateway.getInstance().registerHanlder(
                new EqptRoomHubGateway.WorkInstructionProcedureReportEventHandler(
                    this.onEqptWiProcedureReport)
                );
            EqptRoomHubGateway.getInstance().registerHanlder(
                new EqptRoomHubGateway.WorkInstructionReportEventHandler(
                    this.onEqptWiReport)
                );
            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            eqptRoomHub = new EqptRoomHub("10.31.31.31", 8135);
        }
        public void start()
        {
            Logger.debug("AxisServer: launching servers...");
            businessServer.start();
            wcfServer.start();
            eqptRoomHub.start();
        }
        public void stop()
        {
            Logger.debug("AxisServer: closing servers...");
            businessServer.stop();
            wcfServer.stop();
            eqptRoomHub.stop();
        }

        private void onWCFMessage(object sender, WcfBusinessEventArgs args)
        {
            Logger.info("AxisServer: WcfServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> BusinessServer.");
            Logger.debug("AxisServer: request = {0}/{1}. param = {2}",
                args.context.request.business, args.context.request.method,
                Logger.logObjectList(args.context.request.param));
            args.context.server = businessServer;
            businessServer.postRequest(args.context);
            Logger.info("AxisServer: AxisServer =====> BusinessServer.");
        }

        private void onWorkInstructionProcedureConfirmMessage(object sender, WorkInstructionProcedureConfirmEventArgs args)
        {
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiReportProcedure(args.procedureGuid, args.isSuccess);
        }

        private void onWorkInstructionProceedingMessage(object sender, WorkInstructionEventArgs args)
        {
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiProceeding(args.wiGuid);
        }

        private void onWorkInstructionCompleteMessage(object sender, WorkInstructionEventArgs args)
        {
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiComplete(args.wiGuid);
        }

        private void onWorkInstructionFailMessage(object sender, WorkInstructionEventArgs args)
        {
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiFail(args.wiGuid);
        }

        private void onWorkInstructionDelivery(object sender, WorkInstructionDeliveryEventArgs args)
        {
            eqptRoomHub.deliveryWorkInstrucion(args.workInstructionDeliveryVO);
        }

        private void onEqptWiProcedureReport(object sender, WorkInstructionProcedureReportEventArgs args)
        {
            BusinessRequest request = new BusinessRequest();
            request.business = "workInstruction";
            request.method = "report";
            request.param.Add(args.workInstructionProcedureReportVO.procedureGuid);
            request.param.Add(args.workInstructionProcedureReportVO.isSuccess);
            BusinessResponse response = new testReportResponse();
            BusinessContext ctx = new BusinessContext(request, response);

            businessServer.postRequest(ctx);

        }

        private void onEqptWiReport(object sender, WorkInstructionReportEventArgs args)
        {
            BusinessRequest completeRequest1 = new BusinessRequest();
            completeRequest1.business = "workInstruction";
            completeRequest1.method = "complete";
            completeRequest1.param.Add(args.workInstructionReportVO.workInstructionGuid);
            completeRequest1.param.Add(args.workInstructionReportVO.status);
            BusinessResponse completeResponse1 = new testReportResponse();
            BusinessContext completeCtx1 = new BusinessContext(completeRequest1, completeResponse1);

            businessServer.postRequest(completeCtx1);

        }
    }

    class testReportResponse : BusinessResponse
    {
        public override void onResponsed()
        {

        }
    }
}
