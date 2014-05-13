﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.Action
{
    class IocpReceiveAction : IocpActionBase
    {
        protected Socket socket { get; set; }
        public event EventHandler<IocpReceiveEventArgs> iocpReceiveEvent;
        private const int defaultReceiveBufferSize = 1024 * 4;
        private DescriptorBuffer buffer { get; set; }
        public IocpReceiveAction()
        {
            this.socket = null;
            this.iocpAsyncDelegate = socket.ReceiveAsync;
            buffer = DescriptorBuffer.create(defaultReceiveBufferSize);
        }

        public void attachSocket(Socket socket)
        {
            this.socket = socket;
        }

        public void detachSocket()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Receive);
                this.socket = null;
            }
            // throws if client process has already closed
            catch (Exception) { }
        }

        protected sealed override void onIocpEvent(out bool continousAsyncCall)
        {
            // Check if the remote host closed the connection.
            if (iocpEventArgs.BytesTransferred > 0)
            {
                checkSocketError();

                int recvLength = iocpEventArgs.BytesTransferred;

                if (buffer.desCapacity - buffer.desLength < recvLength)
                {
                    buffer.recapacity((buffer.desLength + recvLength) * 2, true);
                }

                buffer.append(iocpEventArgs.Buffer, iocpEventArgs.Offset, recvLength);

                continousAsyncCall = true;
           
                
            }
            else
            {
                //send null as disconnect event
                IocpReceiveEventArgs args = new IocpReceiveEventArgs(null);
                iocpReceiveEvent(this, args);
                continousAsyncCall = false;
            }
        }


        public void recv()
        {
            iocpOperation();
        }

        public void shutdown()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Receive);
            }
            // throws if client process has already closed
            catch (Exception) { }
        }
    }

    class IocpReceiveEventArgs : EventArgs
    {
        public Descriptor descriptor { get; private set; }
        public IocpReceiveEventArgs(Descriptor descriptor)
        {
            this.descriptor = descriptor;
        }
    }
}