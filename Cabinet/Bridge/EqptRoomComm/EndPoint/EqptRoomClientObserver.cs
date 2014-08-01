using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public interface EqptRoomClientObserver
    {
        void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO);
    }
}
