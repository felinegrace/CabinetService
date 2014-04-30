using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.CommonEntity
{
    public abstract class RawResponse : Jsonable
    {
        public List<object> result { get; private set; }
        public RawResponse()
        {
            result = new List<object>();
            
        }
        public abstract void onResponsed();
    }
}
