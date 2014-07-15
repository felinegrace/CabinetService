using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Utility
{
    public interface SingleListServerInterface<T>
    {
        void postRequest(T request);
    }
}
