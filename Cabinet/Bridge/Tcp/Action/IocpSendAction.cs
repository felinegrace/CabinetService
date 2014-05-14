using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Cabinet.Bridge.Tcp.Action
{
    class IocpSendAction : IocpActionBase
    {
        protected Socket socket { get; set; }

        public IocpSendAction()
        {

        }

        public void attachSocket(Socket socket)
        {
            this.socket = socket;
            this.iocpAsyncDelegate = new IocpAsyncDelegate(socket.SendAsync);
        }

        public void detachSocket()
        {
            this.iocpAsyncDelegate = null;
            try
            { 
                socket.Shutdown(SocketShutdown.Send);
                this.socket = null;
            }
            // throws if client process has already closed
            catch (Exception) { }
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
            
        }
    }
}
