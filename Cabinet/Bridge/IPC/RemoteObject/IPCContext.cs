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
        public class RemoteMessage
        {
            public enum MessageType { Unknown, Synchronized, Asynchronized };
            public MessageType type { get; set; }
            public string descriptor { get; set; }
            public string param { get; set; }

            protected RemoteMessage(string message, string param)
            {
                this.type = MessageType.Unknown;
                this.descriptor = message;
                this.param = param;
            }
        }

        [Serializable]
        public class RemoteMessageSynchronized : RemoteMessage
        {
            public AutoResetEvent notifyEvent = null;
            public RemoteMessageSynchronized(string message, string param)
                : base(message, param)
            {
                this.type = MessageType.Synchronized;
                this.notifyEvent = new AutoResetEvent(false);
            }
        }

        [Serializable]
        public class RemoteMessageAsynchronized : RemoteMessage
        {
            public Guid guid { get; set; }
            public RemoteMessageAsynchronized(string message, string param)
                : base(message, param)
            {
                this.type = MessageType.Asynchronized;
                this.guid = Guid.NewGuid();
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

        #region Synchronized Messaging
        public void sendMessage(string message, string param)
        {
            RemoteMessageSynchronized msg = new RemoteMessageSynchronized(message, param);

            requestQueue.Enqueue(msg);
            serverThreadEvent.Set();

            msg.notifyEvent.WaitOne(-1);
        }
        #endregion

        #region Asynchronized Messaging
        public Guid postMessage(string message, string param)
        {
            RemoteMessageAsynchronized msg = new RemoteMessageAsynchronized(message, param);

            requestQueue.Enqueue(msg);
            serverThreadEvent.Set();
            
            return msg.guid;
        }

        public AutoResetEvent getClientThreadEvent()
        {
            return clientThreadEvent;
        }

        public ConcurrentQueue<RemoteMessage> getResponseQueue()
        {
            return responseQueue;
        }

        /*
        public int getResponseQueueCount()
        {
            return responseQueue.Count;
        }

        public RemoteMessageAsynchronized getResponseQueueFront()
        {
            if (!IPCContext.responseQueue.IsEmpty)
            {
                RemoteMessage msg = null;
                if (IPCContext.responseQueue.TryDequeue(out msg) == true)
                {
                    return msg as RemoteMessageAsynchronized;
                }
            }
            return null;

        }
          
         */
        #endregion
    }
}
