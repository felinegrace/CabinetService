using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cabinet.Bridge.Tcp.IocpContext.IocpAction
{
    abstract class IocpContextBase
    {
        protected delegate bool IocpAsyncDelegate(SocketAsyncEventArgs args);
        protected IocpAsyncDelegate iocpAsyncDelegate;
        protected SocketAsyncEventArgs iocpEventArgs { get; private set; }

        protected IocpContextBase()
        {
            iocpEventArgs = new SocketAsyncEventArgs();
            iocpEventArgs.UserToken = this;
            iocpEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(this.onIocpEventBase);
        }

        protected abstract void onIocpEvent(out bool continousAsyncCall);

        protected void iocpOperation()
        {
            bool continousAsyncCall = true;
            while (continousAsyncCall == true &&
                //false if I/O operation completed synchronously
                iocpAsyncDelegate(iocpEventArgs) == false)
            {
                continousAsyncCall = false;
                onIocpEvent(out continousAsyncCall);
            }
        }

        protected void onIocpEventBase(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
        {
            //incoming param socketAsyncEventArgs should be exactly the same as wrapped iocpEventArgs
            if (socketAsyncEventArgs != iocpEventArgs)
            {
                throw new IocpException("Iocp Event Args not match.");
            }
            bool continousAsyncCall = false;
            onIocpEvent(out continousAsyncCall);
            while (continousAsyncCall == true &&
                //false if I/O operation completed synchronously
                iocpAsyncDelegate(socketAsyncEventArgs) == false)
            {
                continousAsyncCall = false;
                onIocpEvent(out continousAsyncCall);
            }
        }

        protected void checkSocketError()
        {
            if (iocpEventArgs.SocketError == SocketError.OperationAborted)
            {
                //ignored operation cancel
            }
            else if (iocpEventArgs.SocketError != SocketError.Success)
            {
                throw new IocpException(iocpEventArgs.SocketError.ToString());
            }
        }


    }

}
