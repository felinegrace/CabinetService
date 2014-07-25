using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    class MessageParser
    {
        private MessageBase messageBase { get; set; }
        public MessageParser(Descriptor descriptor)
        {
            byte[] pattern = new byte[] { (byte)'\r', (byte)'\n' };
            IEnumerable<int> lineEnds = BytesHelper.indexOf(descriptor.des, 0, descriptor.desLength, pattern); 
            if(lineEnds.Count() < 2)
            {
                throw new EqptRoomCommException("corrupted message.");
            }
            else
            {
                messageBase = new MessageBase();
                messageBase.verb = System.Text.Encoding.ASCII.GetString(
                    descriptor.des, 0, lineEnds.ElementAt(0));
                int payloadOffset = lineEnds.ElementAt(0) + pattern.Length;
                messageBase.payload = System.Text.Encoding.ASCII.GetString(
                    descriptor.des, payloadOffset, lineEnds.ElementAt(1) - payloadOffset);
            }
        }
        public string verb()
        {
            return messageBase.verb;
        }

        public T parseAs<T>()
        {
            return Jsonable.fromJson<T>(messageBase.payload);
        }

        public Register parseAsRegister()
        {
            return Register.fromJson<Register>(messageBase.payload);
        }

        public Acknowledge parseAsAcknowledge()
        {
            return Acknowledge.fromJson<Acknowledge>(messageBase.payload);
        }

        public WorkInstructionDeliveryVO parseAsWorkInstructionDeliveryVO()
        {
            return WorkInstructionDeliveryVO.fromJson<WorkInstructionDeliveryVO>(messageBase.payload);
        }


    }
}
