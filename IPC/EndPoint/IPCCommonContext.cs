using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.IPC.EndPoint
{
    class IPCCommonContext
    {
        public static string channelDescriptor = "Manager2WebServiceBridge";
        public static string objectDescriptor = "RemoteObject";
        public static string fullDescriptor = "ipc://" + channelDescriptor + "/" + objectDescriptor;

    }
}
