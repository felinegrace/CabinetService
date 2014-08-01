using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.EndPoint;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Demo.ClientConsole
{
    class EqptRoomClientConsole : EqptRoomClientObserver
    {
        EqptRoomClient s;
        public void entry()
        {
            s = new EqptRoomClient(this, 
                "127.0.0.1", 6382, "10.31.31.31", 8135);
            s.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S: s.register(new Guid("C9FB1218-5CB6-461D-A7C1-C23AF3EBEEDD"));
                        break;
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            s.stop();
        }

        public void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            UpdateWiStatusVO workInstructionReportVO1 = new UpdateWiStatusVO();
            workInstructionReportVO1.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO1.status = UpdateWiStatusVO.proceeding;
            s.doUpdateWiStatus(workInstructionReportVO1);

            foreach (WorkInstructionProcedureVO workInstructionProcedureVO in workInstructionDeliveryVO.procedureList)
            {
                ReportWiProcedureResultVO workInstructionProcedureReportVO = new ReportWiProcedureResultVO();
                workInstructionProcedureReportVO.procedureGuid = workInstructionProcedureVO.procedureGuid;
                workInstructionProcedureReportVO.isSuccess = true;
                s.doReportWiProcedureResult(workInstructionProcedureReportVO);
            }

            UpdateWiStatusVO workInstructionReportVO2 = new UpdateWiStatusVO();
            workInstructionReportVO2.workInstructionGuid = workInstructionDeliveryVO.wiGuid;
            workInstructionReportVO2.status = UpdateWiStatusVO.complete;
            s.doUpdateWiStatus(workInstructionReportVO2);
        }
    }

    
}
