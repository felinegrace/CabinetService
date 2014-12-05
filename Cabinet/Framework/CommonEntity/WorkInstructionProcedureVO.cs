using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class WorkInstructionProcedureVO : Jsonable
    {
        public Guid procedureGuid { get; set; }
        public string corrCabinetPkId { get; set; }
        public string procedure { get; set; }
    }
}
