using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.Session
{
    interface IocpSessionObserver
    {
        void onSessionConnected(Socket remoteSocket);

        void onSessionData(Guid sessionId, Descriptor descriptor);

        void onSessionDisconnected(Guid sessionId);

        void onSessionError(Guid sessionId, string errorMessage);
    }
}
