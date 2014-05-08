using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cabinet.Bridge.Tcp.IocpContext.IocpAction
{
    class IocpAcceptContext : IocpContextBase
    {
        private Socket listenSocket { get; set; }

        public event EventHandler<IocpAcceptEventArgs> AcceptedEvent;

        public IocpAcceptContext(Socket listenSocket)
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
            continousAsyncCall = true;
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
