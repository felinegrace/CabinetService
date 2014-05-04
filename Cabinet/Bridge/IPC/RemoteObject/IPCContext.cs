using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cabinet.Bridge.IPC.RemoteObject
{
    public class IPCContext : MarshalByRefObject
    {
        #region Messaging objects


        #endregion

        #region Messaging Queues
        public static ConcurrentQueue<IPCMessage> requestQueue = new ConcurrentQueue<IPCMessage>();
        public static ConcurrentQueue<IPCMessage> responseQueue = new ConcurrentQueue<IPCMessage>();

        public static AutoResetEvent serverThreadEvent = new AutoResetEvent(false);
        public static AutoResetEvent clientThreadEvent = new AutoResetEvent(false);

        //public static object requestQueueMutex = new object();
        //public static object responseQueueMutex = new object();
        #endregion

        public void postRequest(IPCMessage message)
        {
            requestQueue.Enqueue(message);
            serverThreadEvent.Set();
        }

        //public AutoResetEvent getServerThreadEvent()
        //{
        //    return serverThreadEvent;
        //}

        public AutoResetEvent getClientThreadEvent()
        {
            return clientThreadEvent;
        }

        
        //public ConcurrentQueue<RemoteMessage> getRequestQueue()
        //{
        //    return requestQueue;
        //}

        public ConcurrentQueue<IPCMessage> getResponseQueue()
        {
            return responseQueue;
        }

    }
}
