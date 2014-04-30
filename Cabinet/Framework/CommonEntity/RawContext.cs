using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.CommonEntity
{
    public class RawContext
    {
        public RawRequest request { get; private set; }
        public RawResponse response { get; private set; }
        public RawContext(RawRequest request, RawResponse response)
        {
            this.request = request;
            this.response = response;
        }
    }
}
