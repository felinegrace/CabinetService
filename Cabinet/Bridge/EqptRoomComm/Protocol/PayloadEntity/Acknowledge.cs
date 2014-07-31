using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity
{
    public class Acknowledge : Jsonable
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
