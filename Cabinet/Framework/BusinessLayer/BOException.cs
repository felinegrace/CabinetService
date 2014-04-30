using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.BusinessLayer
{
    public class BOException : Exception
    {
        public BOException(string message)
            : base(message)
        { 

        }
    }
}
