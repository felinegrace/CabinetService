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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
                new BusinessServerGateway.WorkInstructionProceedingEventHandler(
                this.onWorkInstructionProceedingMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionCompleteEventHandler(
                this.onWorkInstructionCompleteMessage));
            BusinessServerGateway.getInstance().registerHanlder(
                new BusinessServerGateway.WorkInstructionFailEventHandler(
                this.onWorkInstructionFailMessage));
<<<<<<< HEAD
=======
=======
                new BusinessServerGateway.WorkInstructionCompleteEventHandler(
                this.onWorkInstructionCompleteMessage));
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
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
<<<<<<< HEAD
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
=======
<<<<<<< HEAD
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
=======
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiReportProcedure(args.procedureGuid, args.isSuccess);
        }

<<<<<<< HEAD
        private void onWorkInstructionProceedingMessage(object sender, WorkInstructionEventArgs args)
        {
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
=======
<<<<<<< HEAD
        private void onWorkInstructionProceedingMessage(object sender, WorkInstructionEventArgs args)
        {
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiProceeding(args.wiGuid);
        }

        private void onWorkInstructionCompleteMessage(object sender, WorkInstructionEventArgs args)
        {
<<<<<<< HEAD
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
=======
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiComplete(args.wiGuid);
        }

        private void onWorkInstructionFailMessage(object sender, WorkInstructionEventArgs args)
        {
<<<<<<< HEAD
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiFail(args.wiGuid);
=======
            Logger.info("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiFail(args.wiGuid);
=======
        private void onWorkInstructionCompleteMessage(object sender, WorkInstructionCompleteEventArgs args)
        {
            Logger.debug("AxisServer: BusinessServer =====> AxisServer");
            Logger.info("AxisServer: AxisServer - - -> WcfServer.");
            wcfServer.wiComplete(args.wiGuid, args.isSuccess);
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        }
    }
}
