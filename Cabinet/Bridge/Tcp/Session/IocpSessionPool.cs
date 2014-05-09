using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Bridge.Tcp.Session
{
    class IocpSessionPool
    {
        private ConcurrentBag<IocpSession> sessionPoolBag;

        private Func<IocpSession> itemConstructor;

        private event EventHandler<EventArgs> sessionDisposeEvent;
        public IocpSessionPool(Func<IocpSession> itemConstructor)
        {
            if (itemConstructor == null)
            {
                throw new IocpException("session pool item constructor cannot be null.");
            }
            this.sessionPoolBag = new ConcurrentBag<IocpSession>();
            this.itemConstructor = itemConstructor;
            Logger.debug("TcpServer: IocpSessionPool constructed.");
        }

        public void put(IocpSession session)
        {
            if (session == null) 
            {
                throw new IocpException("session put back to pool cannot be null"); 
            }
            sessionDisposeEvent -= session.dispose;
            sessionPoolBag.Add(session);
            Logger.debug("TcpServer: a session has put back into IocpSessionPool ");
        }

        public IocpSession take()
        {
            IocpSession session;
            if (sessionPoolBag.TryTake(out session) == false)
            {
                Logger.debug("requires more context instance from IocpSessionPool, create one.");
                session = itemConstructor();
                session.onSessionDisposedEvent += (sender, e) => {
                    IocpSession disposedSession = sender as IocpSession;
                    put(disposedSession);
                };
            }
            sessionDisposeEvent += session.dispose;
            Logger.debug("TcpServer: a session has took away from IocpSessionPool ");
            return session;
        }

        public void dispose()
        {
            Logger.debug("TcpServer: dispose all sessions.");
            sessionDisposeEvent(this, EventArgs.Empty);
        }

        public int available()
        {
            return sessionPoolBag.Count;
        }


    }
}
