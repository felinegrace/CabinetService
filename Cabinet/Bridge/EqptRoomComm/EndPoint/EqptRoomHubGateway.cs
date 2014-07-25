using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomHubGateway
    {
        private volatile static EqptRoomHubGateway instance = null;
        private static readonly object locker = new object();
        private EqptRoomHubGateway() { }
        public static EqptRoomHubGateway getInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        Logger.debug("EqptCommServer: constructing eqpt room server gateway...");
                        instance = new EqptRoomHubGateway();
                    }
                }
            }
            return instance;
        }

        public delegate void WorkInstructionProcedureReportEventHandler(object sender, WorkInstructionProcedureReportEventArgs args);
        internal event WorkInstructionProcedureReportEventHandler WorkInstructionProcedureReportEvent;

        public delegate void WorkInstructionReportEventHandler(object sender, WorkInstructionReportEventArgs args);
        internal event WorkInstructionReportEventHandler WorkInstructionReportEvent;

        public void registerHanlder(WorkInstructionProcedureReportEventHandler handler)
        {
            WorkInstructionProcedureReportEvent = handler;
        }

        public void registerHanlder(WorkInstructionReportEventHandler handler)
        {
            WorkInstructionReportEvent = handler;
        }

        public void postWorkInstructionProcedureReportEvent(WorkInstructionProcedureReportVO workInstructionProcedureReportVO)
        {
            Logger.debug("BusinessServer: EqptRoomServer - - -> BusinessServer");
            WorkInstructionProcedureReportEvent(this, new WorkInstructionProcedureReportEventArgs(workInstructionProcedureReportVO));
        }

        public void postWorkInstructionProceedingEvent(WorkInstructionReportVO workInstructionReportVO)
        {
            Logger.debug("BusinessServer: EqptRoomServer - - -> BusinessServer");
            WorkInstructionReportEvent(this, new WorkInstructionReportEventArgs(workInstructionReportVO));
        }

    }

    public class WorkInstructionProcedureReportEventArgs : EventArgs
    {
        public WorkInstructionProcedureReportVO workInstructionProcedureReportVO { get; set; }
        public WorkInstructionProcedureReportEventArgs(WorkInstructionProcedureReportVO workInstructionProcedureReportVO)
        {
            this.workInstructionProcedureReportVO = workInstructionProcedureReportVO;
        }
    }

    public class WorkInstructionReportEventArgs : EventArgs
    {
        public WorkInstructionReportVO workInstructionReportVO { get; set; }
        public WorkInstructionReportEventArgs(WorkInstructionReportVO workInstructionReportVO)
        {
            this.workInstructionReportVO = workInstructionReportVO;
        }
    }
}

