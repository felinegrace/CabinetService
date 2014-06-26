﻿using System;
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
        public int wiOperator { get; set; }
        public List<WorkInstructionProcedureVO> procedureList { get; set; }
    }
}
