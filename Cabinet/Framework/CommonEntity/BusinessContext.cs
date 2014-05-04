using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.CommonEntity
{
    public class BusinessContext
    {
        public BusinessRequest request { get; private set; }
        public BusinessResponse response { get; private set; }
        public BusinessContext(BusinessRequest request, BusinessResponse response)
        {
            this.request = request;
            this.response = response;
        }
    }
}
