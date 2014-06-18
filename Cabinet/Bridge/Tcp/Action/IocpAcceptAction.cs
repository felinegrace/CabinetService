using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cabinet.Bridge.Tcp.Action
{
    class IocpAcceptAction : IocpActionBase
    {
        private Socket listenSocket { get; set; }
        private bool continousAccpet { get; set; }
        private Action<Socket> onAcceptedAction { get; set; }

        public IocpAcceptAction(Socket listenSocket, Action<Socket> onAcceptedAction)
        {
            this.listenSocket = listenSocket;
            this.onAcceptedAction = onAcceptedAction;
            this.iocpAsyncDelegate = new IocpAsyncDelegate(listenSocket.AcceptAsync);
        }

        protected sealed override void onIocpEvent(out bool continousAsyncCall)
        {
            checkSocketError();
            if (iocpEventArgs.AcceptSocket.Connected)
            {
                onAcceptedAction(iocpEventArgs.AcceptSocket);
            }
            // Socket must be cleared since the context object is being reused.
            iocpEventArgs.AcceptSocket = null;
            continousAsyncCall = continousAccpet;
        }

        public void accept()
        {
            continousAccpet = true;
            iocpOperation();
        }

        public void shutdown()
        {
            continousAccpet = false;
        }
    }

}
