using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Bridge.EqptRoomComm.Protocol.Parser
{
    class MessageHandler
    {
        private MessageHandlerObserver messageHandlerObserver { get; set; }

        public MessageHandler(MessageHandlerObserver messageHandlerObserver)
        {
            if(messageHandlerObserver == null)
            {
                throw new EqptRoomCommException("messageHandlerObserver is null.");
            }
            this.messageHandlerObserver = messageHandlerObserver;
        }

        internal void handleMessage(Guid sessionId, Descriptor descriptor)
        {
            bool needAcknowledge = true;
            int statusCode = 500;
            string message = "internal error";
            try
            {
                MessageParser parser = new MessageParser(descriptor);
                switch (parser.verb())
                {
                    case "acknowledge":
                        {
                            Acknowledge acknowledge = parser.parseAs<Acknowledge>();
                            messageHandlerObserver.onAcknowledge(sessionId, acknowledge);
                            needAcknowledge = false;
                            break;
                        }
                    case "register":
                        {
                            Register register = parser.parseAs<Register>();
                            messageHandlerObserver.onRegister(sessionId, register);
                            break;
                        }
                    case "delivery":
                        {
                            WorkInstructionDeliveryVO workInstructionDeliveryVO = parser.parseAs<WorkInstructionDeliveryVO>();
                            messageHandlerObserver.onDelivery(sessionId, workInstructionDeliveryVO);
                            break;
                        }
                    case "report":
                        {
                            WorkInstructionProcedureReportVO workInstructionProcedureReportVO = parser.parseAs<WorkInstructionProcedureReportVO>();
                            messageHandlerObserver.onReport(sessionId, workInstructionProcedureReportVO);
                            break;
                        }
                    case "complete":
                        {
                            WorkInstructionReportVO workInstructionReportVO = parser.parseAs<WorkInstructionReportVO>();
                            messageHandlerObserver.onComplete(sessionId, workInstructionReportVO);
                            break;
                        }
                        
                    default:                    
                        throw new EqptRoomCommException("verb error");
                        
                }
                statusCode = 200;
                message = "OK";
            }
            catch (System.Exception ex)
            {
                Logger.error("EqptRoomHub: corrupted data {0}, from session {1}, error: {2}",
                    sessionId, descriptor.des.ToString(), ex.Message);
                statusCode = 400;
                message = "bad request";
            }
            finally
            {
                if(needAcknowledge)
                {
                    Acknowledge acknowledge = new Acknowledge();
                    acknowledge.statusCode = statusCode;
                    acknowledge.message = message;
                    messageHandlerObserver.doAcknowledge(sessionId, acknowledge);
                }
            }
        }
    }
}
