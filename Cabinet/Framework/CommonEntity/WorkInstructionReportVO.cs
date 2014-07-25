using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class WorkInstructionReportVO : Jsonable
    {
        public static string proceeding = "proceeding";
        public static string complete = "complete";
        public static string fail = "fail";
        public Guid workInstructionGuid { get; set; }
        public string status { get; set; }
    }
}
