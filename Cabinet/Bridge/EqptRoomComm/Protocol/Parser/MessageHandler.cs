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
            MessageParser parser = new MessageParser(descriptor);

            try
            {
                while(parser.parseIfHasNext())
                {
                    switch (parser.verb())
                    {
                        case "acknowledge":
                            {
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
                                ReportWiProcedureResultVO workInstructionProcedureReportVO = parser.parseAs<ReportWiProcedureResultVO>();
                                messageHandlerObserver.onReport(sessionId, workInstructionProcedureReportVO);
                                break;
                            }
                        case "complete":
                            {
                                UpdateWiStatusVO workInstructionReportVO = parser.parseAs<UpdateWiStatusVO>();
                                messageHandlerObserver.onComplete(sessionId, workInstructionReportVO);
                                break;
                            }

                        default:
                            throw new EqptRoomCommException("verb error");

                    }
                }      
            }
            catch (System.Exception ex)
            {
                Logger.error("EqptRoomHub: corrupted data {0}, from session {1}, error: {2}",
                    sessionId, descriptor.des.ToString(), ex.Message);
                
            }
        }
    }
}
