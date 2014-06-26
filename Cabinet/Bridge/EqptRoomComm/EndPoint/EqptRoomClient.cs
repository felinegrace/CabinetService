using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    class EqptRoomClient
    {
        private TcpClient tcpClient { get; set; }
        public EqptRoomClient(string clientIpAddress, int clientPort,
            string serverIpAddress, int serverPort)
        {
            tcpClient = new TcpClient(clientIpAddress, clientPort,
                serverIpAddress, serverPort);
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
    }
}
