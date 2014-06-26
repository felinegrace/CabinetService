using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public interface TcpServerObserver
    {
        void onTcpData(Guid sessionId, Descriptor descriptor);
    }
}
