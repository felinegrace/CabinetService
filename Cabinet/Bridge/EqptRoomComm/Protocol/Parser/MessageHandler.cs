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
        private MessageObserver messageHandlerObserver { get; set; }

        public MessageHandler(MessageObserver messageHandlerObserver)
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
                        case "reportWiProcedureResult":
                            {
                                ReportWiProcedureResultVO reportWiProcedureResultVO = parser.parseAs<ReportWiProcedureResultVO>();
                                messageHandlerObserver.onReportWiProcedureResult(sessionId, reportWiProcedureResultVO);
                                break;
                            }
                        case "updateWiStatus":
                            {
                                UpdateWiStatusVO updateWiStatusVO = parser.parseAs<UpdateWiStatusVO>();
                                messageHandlerObserver.onUpdateWiStatus(sessionId, updateWiStatusVO);
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
