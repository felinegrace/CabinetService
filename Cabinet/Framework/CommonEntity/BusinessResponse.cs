using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public abstract class BusinessResponse : Jsonable
    {
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
        public List<object> result { get; private set; }
        public BusinessResponse()
        {
            result = new List<object>();
            
        }
        public abstract void onResponsed();
    }
}
