using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class RegisterMessage : MessageBase
    {
        public RegisterMessage(Register register)
        {
            verb = "register";
            payload = register.toJson();
        }

    }
}
