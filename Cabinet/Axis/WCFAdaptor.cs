﻿using Cabinet.Bridge.WcfService;
using Cabinet.Framework.BusinessLayer;
using Cabinet.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Axis
{
    public class WcfAdaptor
    {
        
        private BusinessServer businessServer { get; set; }
        private WcfServer wcfServer { get; set; }

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
                new BusinessServerGateway.WorkInstructionCompleteEventHandler(
                this.onWorkInstructionCompleteMessage));
            businessServer = new BusinessServer();
            wcfServer = new WcfServer();
            
        }
        public void start()
        {
            Logger.debug("AxisServer: launching servers...");
            businessServer.start();
            wcfServer.start();
        }
        public void stop()
        {
            Logger.debug("AxisServer: closing servers...");
            businessServer.stop();
            wcfServer.stop();
        }

        private void onWCFMessage(object sender, WcfBusinessEventArgs args)
        {
            Logger.debug("AxisServer: WcfServer =====> AxisServer");
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
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiReportProcedure(args.procedureGuid, args.isSuccess);
        }

        private void onWorkInstructionCompleteMessage(object sender, WorkInstructionCompleteEventArgs args)
        {
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiComplete(args.wiGuid, args.isSuccess);
        }
    }
}
