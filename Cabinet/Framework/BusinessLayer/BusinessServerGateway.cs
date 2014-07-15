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

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public delegate void WorkInstructionProceedingEventHandler(object sender, WorkInstructionEventArgs args);
        internal event WorkInstructionProceedingEventHandler WorkInstructionProceedingEvent;

        public delegate void WorkInstructionCompleteEventHandler(object sender, WorkInstructionEventArgs args);
        internal event WorkInstructionCompleteEventHandler WorkInstructionCompleteEvent;

        public delegate void WorkInstructionFailEventHandler(object sender, WorkInstructionEventArgs args);
        internal event WorkInstructionFailEventHandler WorkInstructionFailEvent;
<<<<<<< HEAD
=======
=======
        public delegate void WorkInstructionCompleteEventHandler(object sender, WorkInstructionCompleteEventArgs args);
        internal event WorkInstructionCompleteEventHandler WorkInstructionCompleteEvent;

>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36

        public void registerHanlder(WorkInstructionProcedureConfirmEventHandler handler)
        {
            WorkInstructionProcedureConfirmEvent = handler;
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public void registerHanlder(WorkInstructionProceedingEventHandler handler)
        {
            WorkInstructionProceedingEvent = handler;
        }

<<<<<<< HEAD
=======
=======
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public void registerHanlder(WorkInstructionCompleteEventHandler handler)
        {
            WorkInstructionCompleteEvent = handler;
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public void registerHanlder(WorkInstructionFailEventHandler handler)
        {
            WorkInstructionFailEvent = handler;
        }

<<<<<<< HEAD
=======
=======
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public void postWorkInstructionProcedureConfirmEvent(Guid procedureGuid, bool isSuccess)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionProcedureConfirmEvent(this, new WorkInstructionProcedureConfirmEventArgs(procedureGuid, isSuccess));
        }

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        public void postWorkInstructionProceedingEvent(Guid wiGuid)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionProceedingEvent(this, new WorkInstructionEventArgs(wiGuid));
        }

        public void postWorkInstructionCompleteEvent(Guid wiGuid)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionCompleteEvent(this, new WorkInstructionEventArgs(wiGuid));
        }

        public void postWorkInstructionFailEvent(Guid wiGuid)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionFailEvent(this, new WorkInstructionEventArgs(wiGuid));
<<<<<<< HEAD
=======
=======
        public void postWorkInstructionCompleteEvent(Guid wiGuid, bool isSuccess)
        {
            Logger.debug("BusinessServer: BusinessServer - - -> AxisServer");
            WorkInstructionCompleteEvent(this, new WorkInstructionCompleteEventArgs(wiGuid, isSuccess));
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
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

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
    public class WorkInstructionEventArgs : EventArgs
    {
        public Guid wiGuid { get; set; }
        public WorkInstructionEventArgs(Guid wiGuid)
        {
            this.wiGuid = wiGuid;
<<<<<<< HEAD
=======
=======
    public class WorkInstructionCompleteEventArgs : EventArgs
    {
        public Guid wiGuid { get; set; }
        public bool isSuccess { get; set; }
        public WorkInstructionCompleteEventArgs(Guid wiGuid, bool isSuccess)
        {
            this.wiGuid = wiGuid;
            this.isSuccess = isSuccess;
>>>>>>> ae841d4af93b45a0348747ced1e1879ebb090cb9
>>>>>>> d1c4b94d8d0c2e3a774c703830a9ec1e2cbf1e36
        }
    }
}
