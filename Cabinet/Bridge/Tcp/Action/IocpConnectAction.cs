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

        public event EventHandler<IocpConnectEventArgs> ConnectedEvent;
        public IocpConnectAction(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            this.iocpAsyncDelegate = clientSocket.ConnectAsync;
        }

        protected override void onIocpEvent(out bool continousAsyncCall)
        {
            checkSocketError();
            ConnectedEvent(this, new IocpConnectEventArgs(clientSocket));
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

    class IocpConnectEventArgs : EventArgs
    {
        public Socket socket { get; private set; }
        public IocpConnectEventArgs(Socket socket)
        {
            this.socket = socket;
        }
    }
}
