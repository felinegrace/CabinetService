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
            Logger.debug("IocpSession: constructed.");
        }

        public void attachSocket(Socket socket)
        {
            sessionId = Guid.NewGuid();
            this.socket = socket;
            sendContext.attachSocket(socket);
            recvContext.attachSocket(socket);
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpSession: session {0} starts. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
        }

        public void detachSocket()
        {
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpSession: session {0} ends. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
            sendContext.detachSocket();
            recvContext.detachSocket();
            socket.Close();
            socket = null;
            sessionId = Guid.Empty;
        }

        private void onIocpReceiveContextEvent(object sender, IocpReceiveEventArgs eventArgs)
        {
            Logger.debug("IocpSession: session {0} on receive event ", sessionId);
            if(eventArgs.descriptor == null)
            {
                Logger.debug("IocpSession: session {0} receives nothing as Disconnect signal.", sessionId);
                dispose(this, EventArgs.Empty);
            }
            else
            {
                Logger.debug("IocpSession: session {0} receives {1} bytes of data.", sessionId, eventArgs.descriptor.desLength);
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
            Logger.debug("IocpSession: session {0} disposing...", sessionId);
            detachSocket();
            onSessionDisposedEvent(this, EventArgs.Empty);
        }

    }
}
