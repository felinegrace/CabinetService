using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity
{
    public class Acknowledge : TransactionEntityBase
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public Acknowledge(Guid transactionGuid)
        {
            this.trasactionGuid = trasactionGuid;
        }
    }
}
