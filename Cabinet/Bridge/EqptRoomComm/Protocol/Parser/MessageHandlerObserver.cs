using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    interface MessageHandlerObserver
    {
        void doAcknowledge(Guid sessionId, Acknowledge acknowledge);
        void onRegister(Guid sessionId, Register register);
        void onAcknowledge(Guid sessionId, Acknowledge acknowledge);
    }
}
