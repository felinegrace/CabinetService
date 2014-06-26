using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomHub : TcpServerObserver
    {
        private TcpServer tcpServer { get; set; }
        private EqptRoomClientMap eqptRoomClientMap { get; set; }
        private EqptRoomHubBusiness eqptRoomHubBusiness { get; set; }
        public EqptRoomHub(string ipAddress, int port)
        {
            tcpServer = new TcpServer(ipAddress, port, this);
            eqptRoomClientMap = new EqptRoomClientMap();
            eqptRoomHubBusiness = new EqptRoomHubBusiness();
        }

        public void onTcpData(Guid sessionId, Descriptor descriptor)
        {
            try
            {
                MessageParser parser = new MessageParser(descriptor);
                switch (parser.verb())
                {
                    case "register":
                        {
                            Register register = parser.parseAsRegister();
                            eqptRoomHubBusiness.onRegister(register);
                            break;
                        }
                    default:                    
                        throw new EqptRoomCommException("verb error");
                        
                }
            }
            catch (System.Exception ex)
            {
                Logger.error("EqptRoomHub: corrupted data {0}, from session {1}, error: {2}",
                    sessionId, descriptor.des.ToString(), ex.Message);
            }
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

        private void onEqptRoomClientConnected(Guid eqptRoomGuid, Guid sessionId)
        {
            eqptRoomClientMap.put(eqptRoomGuid, sessionId);
            Logger.debug("EqptRoomHub: new client recgonized: eqpt room guid {0}, session guid {1}",
                eqptRoomGuid, sessionId);
        }

        private void onEqptRoomClientDisconnected(Guid sessionId)
        {
            eqptRoomClientMap.removeBySessionGuid(sessionId);
            Logger.debug("EqptRoomHub: a client offline: session guid {0}",
                sessionId);
        }

        public void dispatchData(Guid eqptRoomGuid, byte[] data, int offset, int count)
        {
            Guid sessionId = eqptRoomClientMap.searchSessionGuid(eqptRoomGuid);
            if(sessionId == Guid.Empty)
            {
                throw new EqptRoomCommException("cannot find session");
            }
            else
            {
                tcpServer.sendData(sessionId, data, offset, count);
            }
        }
    }
}
