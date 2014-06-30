using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.EndPoint
{
    public interface TcpEndPointObserver
    {
        void onTcpData(Guid sessionId, Descriptor descriptor);
    }
}
