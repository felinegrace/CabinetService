using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class WorkInstructionProcedureVO : Jsonable
    {
        public string procedure { get; set; }
        public Guid corrCabinetGuid { get; set; }
    }
}
