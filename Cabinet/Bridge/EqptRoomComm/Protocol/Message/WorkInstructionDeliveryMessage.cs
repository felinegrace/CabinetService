using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class WorkInstructionDeliveryMessage : MessageBase
    {
        public WorkInstructionDeliveryMessage(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            verb = "delivery";
            payload = workInstructionDeliveryVO.toJson();
        }
    }
}
