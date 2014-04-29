using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.CommonEntity
{
    public class RawRequest : Jsonable
    {
        public string method { get; set; }
        public List<object> param { get; private set; }
        public RawRequest()
        {
            param = new List<object>();
        }
    }
}
