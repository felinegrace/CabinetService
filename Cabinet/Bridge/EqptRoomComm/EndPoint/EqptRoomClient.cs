using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomClient : TcpEndPointObserver, MessageHandlerObserver
    {
        private TcpClient tcpClient { get; set; }
        private MessageHandler messageHandler { get; set; }
        public EqptRoomClient(string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            tcpClient = new TcpClient(clientIpAddress, clientPort,
                serverIpAddress, serverPort, this);
            messageHandler = new MessageHandler(this);
        }

        public void start()
        {
            Logger.debug("EqptRoomClient: starting...");
            tcpClient.start();
            Logger.debug("EqptRoomClient: start.");
        }

        public void stop()
        {
            Logger.debug("EqptRoomClient: stopping...");
            tcpClient.stop();
            Logger.debug("EqptRoomClient: stop.");
        }

        public void register(Guid eqptRoomGuid)
        {
            Register registerEntity = new Register();
            registerEntity.eqptRoomGuid = eqptRoomGuid;
            RegisterMessage registerMessage = new RegisterMessage(registerEntity);
            tcpClient.send(registerMessage.rawMessage());
        }

        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        void MessageHandlerObserver.doAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            AcknowledgeMessage acknowledgeMessage = new AcknowledgeMessage(acknowledge);
            tcpClient.send(acknowledgeMessage.rawMessage());
        }

        void MessageHandlerObserver.onRegister(Guid sessionId, Register register)
        {
            throw new EqptRoomCommException("clinet not supported.");
        }

        void MessageHandlerObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            Logger.debug("EqptRoomHubBusiness: server reports {0},{1}.",
                acknowledge.statusCode, acknowledge.message);
        }

    }
}
