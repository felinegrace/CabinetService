using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Cabinet.Bridge.IPC.RemoteObject
{
    class IPCContext : MarshalByRefObject
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
        public static Queue<RemoteMessage> requestQueue = new Queue<RemoteMessage>();
        public static Queue<RemoteMessage> responseQueue = new Queue<RemoteMessage>();

        public static AutoResetEvent serverThreadEvent = new AutoResetEvent(false);
        public static AutoResetEvent clientThreadEvent = new AutoResetEvent(false);

        public static object requestQueueMutex = new object();
        public static object responseQueueMutex = new object();
        #endregion

        #region Synchronized Messaging
        public void sendMessage(string message, string param)
        {
            RemoteMessageSynchronized msg = new RemoteMessageSynchronized(message, param);
            lock (IPCContext.requestQueueMutex)
            {
                requestQueue.Enqueue(msg);
                serverThreadEvent.Set();
            }
            msg.notifyEvent.WaitOne(-1);
        }
        #endregion

        #region Asynchronized Messaging
        public Guid postMessage(string message, string param)
        {
            RemoteMessageAsynchronized msg = new RemoteMessageAsynchronized(message, param);
            lock (IPCContext.requestQueueMutex)
            {
                requestQueue.Enqueue(msg);
                serverThreadEvent.Set();
            }
            return msg.guid;
        }

        public AutoResetEvent getClientThreadEvent()
        {
            return clientThreadEvent;
        }

        public int getResponseQueueCount()
        {
            return responseQueue.Count;
        }

        public RemoteMessageAsynchronized getResponseQueueFront()
        {
            RemoteMessageAsynchronized msg = null;
            lock (IPCContext.requestQueueMutex)
            {
                msg = IPCContext.responseQueue.Dequeue() as RemoteMessageAsynchronized;
            } 
            return msg;

        }
        #endregion
    }
}
