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
        public Guid sessionId { get; set; }
        private Socket socket { get; set; }
        private IocpSendAction sendAction { get; set; }
        private IocpReceiveAction recvAction { get; set; }
        private IIocpSessionObserver observer { get; set; }

        public IocpSession(IIocpSessionObserver observer)
        {
            this.observer = observer;
            sendAction = new IocpSendAction(
                ((bytesSent) => this.onIocpSendActionEvent(bytesSent)));
            recvAction = new IocpReceiveAction(
                (descriptor) => this.onIocpReceiveActionEvent(descriptor));
            Logger.debug("IocpSession: constructed.");
        }

        public void attachSocket(Socket socket)
        {
            this.socket = socket;
            sendAction.attachSocket(socket);
            recvAction.attachSocket(socket);
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpSession: session {0} starts. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
        }

        public void detachSocket()
        {
            IPEndPoint remoteIpEndPoint = socket.RemoteEndPoint as IPEndPoint;
            Logger.info("TcpSession: session {0} ends. remote address = {1}:{2}",
                sessionId, remoteIpEndPoint.Address, remoteIpEndPoint.Port);
            sendAction.detachSocket();
            recvAction.detachSocket();
            socket.Close();
            socket = null;
        }

        private void onIocpReceiveActionEvent(Descriptor descriptor)
        {
            Logger.debug("IocpSession: session {0} on receive event ", sessionId);
            if (descriptor == null)
            {
                Logger.debug("IocpSession: session {0} receives nothing as Disconnect signal.", sessionId);
                dispose(this, EventArgs.Empty);
            }
            else
            {
                Logger.debug("IocpSession: session {0} receives {1} bytes of data. ascii data: {2}",
                    sessionId, descriptor.desLength, descriptor.toString(0, descriptor.desLength));
                digest(descriptor);
            }
        }

        private void onIocpSendActionEvent(int bytesSent)
        {
            Logger.debug("IocpSession: session {0} sends {1} byte(s) of data.", sessionId, bytesSent);
        }

        private void digest(Descriptor descriptor)
        {
            observer.onSessionData(sessionId, descriptor);
        }

        public void send(byte[] buffer, int offset, int count)
        {
            sendAction.send(buffer, offset, count);
        }

        public void recv()
        {
            recvAction.recv();
        }

        public void dispose(object sender, EventArgs args)
        {
            detachSocket();
            Logger.debug("IocpSession: session {0} disposed.", sessionId);
            observer.onSessionDisconnected(sessionId);
        }

    }

}
