using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.BusinessLayer
{
    public class BusinessServerGateway
    {
        private volatile static BusinessServerGateway instance = null;
        private static readonly object locker = new object();
        private BusinessServerGateway() { }
        public static BusinessServerGateway getInstance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        Logger.debug("WcfServer: constructing business server gateway...");
                        instance = new BusinessServerGateway();
                    }
                }
            }
            return instance;
        }

        public delegate void WorkInstructionProcedureConfirmEventHandler(object sender, WorkInstructionProcedureConfirmEventArgs args);
        internal event WorkInstructionProcedureConfirmEventHandler WorkInstructionProcedureConfirmEvent;

        public delegate void WorkInstructionCompleteEventHandler(object sender, WorkInstructionCompleteEventArgs args);
        internal event WorkInstructionCompleteEventHandler WorkInstructionCompleteEvent;


        public void registerHanlder(WorkInstructionProcedureConfirmEventHandler handler)
        {
            WorkInstructionProcedureConfirmEvent = handler;
        }

        public void registerHanlder(WorkInstructionCompleteEventHandler handler)
        {
            WorkInstructionCompleteEvent = handler;
        }

        public void postWorkInstructionProcedureConfirmEvent(Guid procedureGuid, bool isSuccess)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionProcedureConfirmEvent(this, new WorkInstructionProcedureConfirmEventArgs(procedureGuid, isSuccess));
        }

        public void postWorkInstructionCompleteEvent(Guid wiGuid, bool isSuccess)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionCompleteEvent(this, new WorkInstructionCompleteEventArgs(wiGuid, isSuccess));
        }
    }

    public class WorkInstructionProcedureConfirmEventArgs : EventArgs
    {
        public Guid procedureGuid { get; set; }
        public bool isSuccess { get; set; }
        public WorkInstructionProcedureConfirmEventArgs(Guid procedureGuid, bool isSuccess)
        {
            this.procedureGuid = procedureGuid;
            this.isSuccess = isSuccess;
        }
    }

    public class WorkInstructionCompleteEventArgs : EventArgs
    {
        public Guid wiGuid { get; set; }
        public bool isSuccess { get; set; }
        public WorkInstructionCompleteEventArgs(Guid wiGuid, bool isSuccess)
        {
            this.wiGuid = wiGuid;
            this.isSuccess = isSuccess;
        }
    }
}
