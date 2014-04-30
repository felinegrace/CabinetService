using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Utility
{
    public abstract class SingleListServer<T> : EventablePollingThread
    {
        private List<T> requestList { get; set; }
        public SingleListServer(List<T> requestList)
        {
            this.requestList = requestList;
        }

        public override void onEventablePoll()
        {
            throw new NotImplementedException();
        }
    }
}
