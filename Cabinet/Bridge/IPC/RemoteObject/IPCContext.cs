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
        [Serializable]
        public class RemoteMessage : EventArgs
        {
            public Guid guid { get; set; }
            public string business { get; set; }
            public string method { get; set; }
            public string param { get; set; }
            public string result { get; set; }

            //used for send method
            public AutoResetEvent notifyEvent { get; private set; }

            private RemoteMessage(bool needEvent, string business, string method, string param)
            {
                if(needEvent)
                {
                    this.notifyEvent = new AutoResetEvent(false);
                }
                else
                {
                    this.notifyEvent = null;
                }
                this.guid = Guid.NewGuid();
                this.business = business;
                this.method = method;
                this.param = param;
            }

        }

        #endregion

        #region Messaging Queues
        public static ConcurrentQueue<RemoteMessage> requestQueue = new ConcurrentQueue<RemoteMessage>();
        public static ConcurrentQueue<RemoteMessage> responseQueue = new ConcurrentQueue<RemoteMessage>();

        public static AutoResetEvent serverThreadEvent = new AutoResetEvent(false);
        public static AutoResetEvent clientThreadEvent = new AutoResetEvent(false);

        //public static object requestQueueMutex = new object();
        //public static object responseQueueMutex = new object();
        #endregion

        public void postRequest(RemoteMessage message)
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

        public ConcurrentQueue<RemoteMessage> getResponseQueue()
        {
            return responseQueue;
        }

    }
}
