using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Message
{
    class MessageBase
    {
        public string verb { get; set; }
        public string payload { get; set; }

        public string rawMessage()
        {
            return verb + "\r\n" + payload + "\r\n" + " \r\n";
        }

        public byte[] rawBytes()
        {
            return System.Text.Encoding.Default.GetBytes(rawMessage());
        }
    }
}
