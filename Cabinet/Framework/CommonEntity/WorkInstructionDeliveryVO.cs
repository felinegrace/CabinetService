using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class WorkInstructionDeliveryVO : Jsonable
    {
        public Guid wiGuid { get; set; }
        public Guid eqptRoomGuid { get; set; }
        public string wiCardNum { get; set; }
        public string wiOperator { get; set; }
        public DateTime wiOperStartTime { get; set; }
        public DateTime wiOperEndTime { get; set; }
        public List<WorkInstructionProcedureVO> procedureList { get; set; }
    }
}
