using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomHub : TcpEndPointObserver, MessageHandlerObserver
    {
        private TcpServer tcpServer { get; set; }
        private EqptRoomClientMap eqptRoomClientMap { get; set; }
        private MessageHandler messageHandler { get; set; }
        public EqptRoomHub(string ipAddress, int port)
        {
            tcpServer = new TcpServer(ipAddress, port, this);
            eqptRoomClientMap = new EqptRoomClientMap();
            messageHandler = new MessageHandler(this);
        }

        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            messageHandler.handleMessage(sessionId, descriptor);
        }

        public void start()
        {
            Logger.debug("EqptRoomHub: starting...");
            tcpServer.start();
            Logger.debug("EqptRoomHub: start.");
        }

        public void stop()
        {
            Logger.debug("EqptRoomHub: stopping...");
            tcpServer.stop();
            Logger.debug("EqptRoomHub: stop.");
        }

        public void dispatchDataByEqptRoomGuid(Guid eqptRoomGuid, byte[] data, int offset, int count)
        {
            Guid sessionGuid = eqptRoomClientMap.searchSessionGuid(eqptRoomGuid);
            if (sessionGuid == Guid.Empty)
            {
                throw new EqptRoomCommException("cannot find session");
            }
            else
            {
                dispatchDataBySessionGuid(sessionGuid, data, offset, count);
            }
        }

        public void dispatchDataBySessionGuid(Guid sessionGuid, byte[] data, int offset, int count)
        {
            if (sessionGuid == Guid.Empty)
            {
                throw new EqptRoomCommException("error session guid.");
            }
            else
            {
                tcpServer.sendData(sessionGuid, data, offset, count);
            }
        }

        void MessageHandlerObserver.onRegister(Guid sessionId, Register register)
        {
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} request register.",
                register.eqptRoomGuid);
            eqptRoomClientMap.put(register.eqptRoomGuid, sessionId);
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} register complete.",
                register.eqptRoomGuid);
        }

        void MessageHandlerObserver.doAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            AcknowledgeMessage acknowledgeMessage = new AcknowledgeMessage(acknowledge);
            byte[] acknowledgeBytes = System.Text.Encoding.ASCII.GetBytes(acknowledgeMessage.rawMessage());
            dispatchDataBySessionGuid(sessionId, acknowledgeBytes, 0, acknowledgeBytes.Length);
        }

        void MessageHandlerObserver.onAcknowledge(Guid sessionId, Acknowledge acknowledge)
        {
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} reports {1},{2}.",
                sessionId, acknowledge.statusCode, acknowledge.message);
        }
    }
}
