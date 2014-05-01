using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Utility;
namespace Cabinet.Framework.BusinessLayer
{
    public class BOException : Exception
    {
        public BOException(string message)
            : base(message)
        {
            Logger.error(message);
        }
    }
}
