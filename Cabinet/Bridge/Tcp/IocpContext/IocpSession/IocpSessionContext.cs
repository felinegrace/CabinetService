using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Cabinet.Bridge.Tcp.IocpContext.IocpAction;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.IocpContext.IocpSession
{
    class IocpSessionContext
    {
        private Socket socket;
        private IocpSendContext sendContext;
        private IocpReceiveContext recvContext;

        public IocpSessionContext(Socket socket)
        {
            this.socket = socket;
            sendContext = new IocpSendContext(socket);
            recvContext = new IocpReceiveContext(socket);
        }

        private void setContextObserver()
        {
            recvContext.iocpReceiveEvent += new EventHandler<IocpReceiveEventArgs>(this.onIocpReceiveContextEvent);
        }

        private void onIocpReceiveContextEvent(object sender, IocpReceiveEventArgs eventArgs)
        {
            if(eventArgs.descriptor == null)
            {
                onDisconnect();
            }
            else
            {
                onReceive(eventArgs.descriptor);
            }
        }

        private void onDisconnect()
        {
            sendContext.shutdown();
            recvContext.shutdown();
            socket.Close();
        }

        private void onReceive(Descriptor descriptor)
        {

        }

        public void send(byte[] buffer, int offset, int count)
        {
            sendContext.send(buffer, offset, count);
        }

        public void recv()
        {
            recvContext.recv();
        }
    }
}
