using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Cabinet.Bridge.Tcp.Action;
using Cabinet.Utility;
using System.Net;

namespace Cabinet.Bridge.Tcp.Session
{
    class IocpSession
    {
        public Guid sessionId { get; private set; }
        private Socket socket { get; set; }
        private IocpSendAction sendContext { get; set; }
        private IocpReceiveAction recvContext { get; set; }

        internal event EventHandler<EventArgs> onSessionDisposedEvent;

        public IocpSession()
        {
            sendContext = new IocpSendAction();
            recvContext = new IocpReceiveAction();
            recvContext.iocpReceiveEvent += ((sender, e) =>
                this.onIocpReceiveContextEvent(sender, e) );
        }

        public void attachSocket(Socket socket)
        {
            sessionId = Guid.NewGuid();
            this.socket = socket;
            sendContext.attachSocket(socket);
            recvContext.attachSocket(socket);
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpServer: session {0} starts. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
        }

        public void detachSocket()
        {
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpServer: session {0} ends. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
            sendContext.detachSocket();
            recvContext.detachSocket();
            socket.Close();
            socket = null;
            sessionId = Guid.Empty;
        }

        private void onIocpReceiveContextEvent(object sender, IocpReceiveEventArgs eventArgs)
        {
            if(eventArgs.descriptor == null)
            {
                dispose(this, EventArgs.Empty);
            }
            else
            {
                onReceive(eventArgs.descriptor);
            }
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

        public void dispose(object sender, EventArgs args)
        {
            detachSocket();
            onSessionDisposedEvent(this, EventArgs.Empty);
        }

    }
}
