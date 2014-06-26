using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class BusinessRequest : Jsonable
    {
        public string business { get; set; }
        public string method { get; set; }
        public List<object> param { get; private set; }
        public BusinessRequest()
        {
            param = new List<object>();
        }
    }
}
