using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class ReportWiProcedureResultVO : Jsonable
    {
        public Guid procedureGuid { get; set; }

        public bool isSuccess { get; set; }
    }
}
