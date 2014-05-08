using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cabinet.Bridge.Tcp.IocpContext.IocpAction
{
    class IocpSendContext : IocpContextBase
    {
        protected Socket socket { get; set; }
        public IocpSendContext(Socket socket)
        {
            this.socket = socket;
            this.iocpAsyncDelegate = socket.SendAsync;
        }

        protected sealed override void onIocpEvent(out bool continousAsyncCall)
        {
            checkSocketError();
            if (iocpEventArgs.BytesTransferred != iocpEventArgs.Buffer.Length)
            {
                throw new IocpException("not all datas are sent.");
            }
            continousAsyncCall = false;
        }

        public void send(byte[] buffer, int offset, int count)
        {
            if (buffer == null || count <= 0)
            {
                throw new IocpException("sending a null data.");
            }
            iocpEventArgs.SetBuffer(buffer, offset, count);
            iocpOperation();
        }

        public void shutdown()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Send);
            }
            // throws if client process has already closed
            catch (Exception) { }
        }
    }
}
