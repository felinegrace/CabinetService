using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.CommonModuleEntry
{
    public interface EqptRoomCommModuleEntry
    {
        void acknowledge(Guid transactionGuid, Guid eqptRoomGuid, int statusCode, string message);
        void deliveryWorkInstrucion(WorkInstructionDeliveryVO workInstructionDeliveryVO);
    }
}
