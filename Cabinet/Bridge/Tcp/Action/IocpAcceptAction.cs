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

        public event EventHandler<IocpAcceptEventArgs> AcceptedEvent;

        public IocpAcceptAction(Socket listenSocket)
        {
            this.listenSocket = listenSocket;
            this.iocpAsyncDelegate = listenSocket.AcceptAsync;
        }

        protected sealed override void onIocpEvent(out bool continousAsyncCall)
        {
            checkSocketError();
            if (iocpEventArgs.AcceptSocket.Connected)
            {
                AcceptedEvent(this, new IocpAcceptEventArgs(iocpEventArgs.AcceptSocket));
            }
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

    class IocpAcceptEventArgs : EventArgs
    {
        public Socket socket { get; private set; }
        public IocpAcceptEventArgs(Socket socket)
        {
            this.socket = socket;
        }
    }
}
