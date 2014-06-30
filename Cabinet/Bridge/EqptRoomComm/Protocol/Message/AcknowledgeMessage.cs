using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class AcknowledgeMessage : MessageBase
    {
        public AcknowledgeMessage(Acknowledge acknowledge)
        {
            verb = "acknowledge";
            payload = acknowledge.toJson();
        }
    }
}
