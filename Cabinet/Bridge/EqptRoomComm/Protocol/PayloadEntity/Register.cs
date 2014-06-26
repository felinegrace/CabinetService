using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity
{
    class Register : Jsonable
    {
        public Guid eqptRoomGuid { get; set; }
    }
}
