using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public interface EqptRoomClientObserver
    {
        void onEqptRoomHubConnected();
        void onEqptRoomHubDisconnected();
        void onEqptRoomHubCommunicationError(string errorMessage);
        void onAcknowledge(Acknowledge acknowledge);
        void onWorkInstrucionDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO);
    }
}
