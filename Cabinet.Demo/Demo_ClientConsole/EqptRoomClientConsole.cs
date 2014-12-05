using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.EndPoint;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Demo.ClientConsole
{
    class EqptRoomClientConsole : EqptRoomClientObserver
    {
        EqptRoomClient s;
        Guid eqptRoomGuid;
        public void entry()
        {
            eqptRoomGuid = new Guid("C9FB1218-5CB6-461D-A7C1-C23AF3EBEEDD");
            s = new EqptRoomClient(this, 
                "10.31.31.31", 6382, "10.31.31.31", 8135);
            s.start();
            
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S:
                        {
                            Guid logUsedGuid = s.doRegister(eqptRoomGuid);
                            Logger.debug("a doRegister submitted with transId {0}.", logUsedGuid);
                            break;
                        }
                    case ConsoleKey.C:
                        {
                            s.start();
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            s.stop();
                            break;
                        }
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }

        public void onAcknowledge(Acknowledge acknowledge)
        {
            Logger.debug("transaction {0} returns result {1} with message {2} in EqptRoom {3}",
                acknowledge.trasactionGuid,
                acknowledge.statusCode,
                acknowledge.message,
                acknowledge.eqptRoomGuid);
        }

        public void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            Guid logUsedGuid;

            UpdateWiStatusVO workInstructionReportVO1 = new UpdateWiStatusVO();
            workInstructionReportVO1.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO1.status = UpdateWiStatusVO.proceeding;
            logUsedGuid = s.doUpdateWiStatus(eqptRoomGuid, workInstructionReportVO1);
            Logger.debug("a doUpdateWiStatus submitted with transId {0}.",
                logUsedGuid);

            //no longer needed
            /*
            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                ReportWiProcedureResultVO workInstructionProcedureReportVO = new ReportWiProcedureResultVO();
                workInstructionProcedureReportVO.procedureGuid = workInstructionProcedureVO.procedureGuid;
                workInstructionProcedureReportVO.isSuccess = true;
                logUsedGuid = s.doReportWiProcedureResult(eqptRoomGuid, workInstructionProcedureReportVO);
                Logger.debug("a doReportWiProcedureResult submitted with transId {0}.",
                    logUsedGuid);
            }
            */

            UpdateWiStatusVO workInstructionReportVO2 = new UpdateWiStatusVO();
            workInstructionReportVO2.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO2.status = UpdateWiStatusVO.complete;
            logUsedGuid = s.doUpdateWiStatus(eqptRoomGuid, workInstructionReportVO2);
            Logger.debug("a doUpdateWiStatus submitted with transId {0}.",
                logUsedGuid);
        }

        public void onEqptRoomHubConnected()
        {
            Logger.debug("server connected!!!");
            //s.stop();
        }

        public void onEqptRoomHubDisconnected()
        {
            Logger.debug("server disconnected!!!");
            //s.start();
        }


        public void onEqptRoomHubCommunicationError(string errorMessage)
        {
            Logger.error("server error!!!  {0}", errorMessage);
            s.stop();
            s.start();
        }
    }

    
}
