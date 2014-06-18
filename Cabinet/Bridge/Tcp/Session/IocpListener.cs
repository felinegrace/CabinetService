using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Cabinet.Utility;
using Cabinet.Bridge.Tcp.Action;

namespace Cabinet.Bridge.Tcp.Session
{
    class IocpListener
    {
        private IPEndPoint listenerEndPoint { get; set; }
        private Socket listenerSocket { get; set; }
        private IocpAcceptAction acceptAction { get; set; }

        private const int initialPendingConnectionCount = 16;

        public IocpListener(IPEndPoint listenerEndPoint, IIocpSessionObserver observer)
        {
            this.listenerEndPoint = listenerEndPoint;
            this.listenerSocket = new Socket(listenerEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            acceptAction = new IocpAcceptAction(listenerSocket,
                ((socket) => { observer.onSessionConnected(socket); }));
        }
        public void start()
        {
            if (listenerEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // Set dual-mode (IPv4 & IPv6) for the socket listener.
                // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
                // based on http://blogs.msdn.com/wndp/archive/2006/10/24/creating-ip-agnostic-applications-part-2-dual-mode-sockets.aspx
                listenerSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                listenerSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, listenerEndPoint.Port));
            }
            else
            {
                // Associate the socket with the local endpoint.
                listenerSocket.Bind(listenerEndPoint);
            }
            Logger.debug("IocpListener: address {0}:{1} binded.", listenerEndPoint.Address.ToString(), listenerEndPoint.Port);
            listenerSocket.Listen(initialPendingConnectionCount);
            Logger.debug("IocpListener: address {0}:{1} start listen.", listenerEndPoint.Address.ToString(), listenerEndPoint.Port);
            acceptAction.accept();
            Logger.debug("IocpListener: address {0}:{1} srart accept.", listenerEndPoint.Address.ToString(), listenerEndPoint.Port);
        }

        public void stop()
        {
            Logger.debug("IocpListener: address {0}:{1} stopping.", listenerEndPoint.Address.ToString(), listenerEndPoint.Port);
            acceptAction.shutdown();
            listenerSocket.Close();
            Logger.debug("IocpListener: address {0}:{1} stop.", listenerEndPoint.Address.ToString(), listenerEndPoint.Port);      
        }
    }
}
