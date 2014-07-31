using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Bridge.Tcp.EndPoint;
using Cabinet.Utility;
using Cabinet.Bridge.EqptRoomComm.Protocol.Parser;
using Cabinet.Bridge.EqptRoomComm.Protocol.PayloadEntity;
using Cabinet.Bridge.EqptRoomComm.Protocol.Message;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.CommonModuleEntry;

namespace Cabinet.Bridge.EqptRoomComm.EndPoint
{
    public class EqptRoomHub : EqptRoomHubMessageExchanger, TcpEndPointObserver, EqptRoomCommModuleEntry
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

        public void deliveryWorkInstrucion(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            doDelivery(workInstructionDeliveryVO);
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

        protected sealed override void onRegisterMessage(Guid sessionId, Register register)
        {
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} request register.",
                register.eqptRoomGuid);
            eqptRoomClientMap.put(register.eqptRoomGuid, sessionId);
            Logger.debug("EqptRoomHubBusiness: eqpt room guid {0} register complete.",
                register.eqptRoomGuid);
        }

        protected sealed override void doDelivery(WorkInstructionDeliveryVO workInstructionDeliveryVO)
        {
            WorkInstructionDeliveryMessage workInstructionDeliveryMessage = new WorkInstructionDeliveryMessage(workInstructionDeliveryVO);
            byte[] workInstructionDeliveryBytes = System.Text.Encoding.ASCII.GetBytes(workInstructionDeliveryMessage.rawMessage());
            dispatchDataByEqptRoomGuid(workInstructionDeliveryVO.eqptRoomGuid, workInstructionDeliveryBytes, 0, workInstructionDeliveryBytes.Length);
        }

        protected sealed override void onReportWiProcedureResult(Guid sessionId, ReportWiProcedureResultVO reportWiProcedureResultVO)
        {
            new WorkInstrucionServiceBusinessImpl().reportWiProcedureResult(reportWiProcedureResultVO);
        }

        protected sealed override void onUpdateWiStatus(Guid sessionId, UpdateWiStatusVO updateWiStatusVO)
        {
            new WorkInstrucionServiceBusinessImpl().updateWiStatus(updateWiStatusVO);
        }


    }
}
