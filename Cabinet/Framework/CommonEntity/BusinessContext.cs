using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.CommonEntity
{
    public class BusinessContext
    {
        public RawRequest request { get; private set; }
        public RawResponse response { get; private set; }
        public BusinessContext(RawRequest request, RawResponse response)
        {
            this.request = request;
            this.response = response;
        }
    }
}
