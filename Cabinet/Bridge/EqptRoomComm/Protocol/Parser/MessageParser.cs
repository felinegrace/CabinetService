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
        private Descriptor descriptor { get; set; }
        private IEnumerable<int> lineEnds { get; set; }
        private int lineEndsIndex { get; set; }
        private int lineEndsCount { get; set; }
        private MessageBase messageBase { get; set; }

        private static int lineEndsInEachMessage = 3;

        private static int lineEndsPatternLength = 2;
        public MessageParser(Descriptor descriptor)
        {
            lineEndsIndex = 0;
            messageBase = new MessageBase();
            this.descriptor = descriptor;
            byte[] pattern = new byte[] { (byte)'\r', (byte)'\n' };
            lineEnds = BytesHelper.indexOf(descriptor.des, 0, descriptor.desLength, pattern);
            lineEndsCount = lineEnds.Count<int>();

        }

        public bool parseIfHasNext()
        {
            if (lineEndsIndex > lineEndsCount)
            {
                throw new EqptRoomCommException("corrupted message.");
            }
            if(lineEndsIndex == lineEndsCount)
            {
                return false;
            }

            int payloadOffset = lineEndsIndex == 0 ?
                0 :
                lineEnds.ElementAt(lineEndsIndex - 1) + lineEndsPatternLength;

            messageBase.verb = System.Text.Encoding.Default.GetString(
                descriptor.des, payloadOffset, lineEnds.ElementAt(lineEndsIndex) - payloadOffset);
            payloadOffset = lineEnds.ElementAt(lineEndsIndex) + lineEndsPatternLength;
            messageBase.payload = System.Text.Encoding.Default.GetString(
                descriptor.des, payloadOffset, lineEnds.ElementAt(lineEndsIndex + 1) - payloadOffset);
            lineEndsIndex += lineEndsInEachMessage;
            return true;
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
