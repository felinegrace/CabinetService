using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Cabinet.Bridge.Tcp.Action
{
    class IocpConnectAction : IocpActionBase
    {
        private Socket clientSocket { get; set; }
        private Action<Socket> onConnectedAction { get; set; }

        public IocpConnectAction(Socket clientSocket, Action<Socket> onConnectedAction)
        {
            this.clientSocket = clientSocket;
            this.onConnectedAction = onConnectedAction;
            this.iocpAsyncDelegate = new IocpAsyncDelegate(clientSocket.ConnectAsync);
        }

        protected override void onIocpEvent(out bool continousAsyncCall)
        {
            checkSocketError();
            onConnectedAction(clientSocket);
            continousAsyncCall = false;
        }

        public void connect(IPEndPoint serverEndPoint)
        {
            iocpEventArgs.RemoteEndPoint = serverEndPoint;
            iocpOperation();
        }

        public void shutdown()
        {
            clientSocket.Disconnect(false);
        }
    }

}
