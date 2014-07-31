using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.CommonModuleEntry
{
    public interface WcfServiceModuleEntry
    {
        void reportWiProcedureResult(ReportWiProcedureResultVO reportWiProcedureResultVO);
        void updateWiStatusAsProceeding(Guid wiGuid);
        void updateWiStatusAsComplete(Guid wiGuid);
        void updateWiStatusAsFail(Guid wiGuid);
    }
}
