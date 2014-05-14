using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Cabinet.Bridge.Tcp.Action;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    class IocpListener
    {
        private IPEndPoint listenerEndPont { get; set; }
        private Socket listenerSocket { get; set; }
        private IocpAcceptAction acceptAction { get; set; }
        

        private const int initialPendingConnectionCount = 16;

        public void registerAcceptEventHanlder(EventHandler<IocpAcceptEventArgs> handler)
        {
            acceptAction.AcceptedEvent += handler;
        }

        public IocpListener(IPEndPoint listenerEndPont)
        {
            this.listenerEndPont = listenerEndPont;
            this.listenerSocket = new Socket(listenerEndPont.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            acceptAction = new IocpAcceptAction(listenerSocket);
        }
        public void start()
        {
            if (listenerEndPont.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // Set dual-mode (IPv4 & IPv6) for the socket listener.
                // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
                // based on http://blogs.msdn.com/wndp/archive/2006/10/24/creating-ip-agnostic-applications-part-2-dual-mode-sockets.aspx
                listenerSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                listenerSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, listenerEndPont.Port));
            }
            else
            {
                // Associate the socket with the local endpoint.
                listenerSocket.Bind(listenerEndPont);
            }

            listenerSocket.Listen(initialPendingConnectionCount);
            acceptAction.accept();
        }

        public void stop()
        {
            acceptAction.shutdown();
            listenerSocket.Close();
        }
    }
}
