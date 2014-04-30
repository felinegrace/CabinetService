using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Framework.PersistenceLayer
{
    class DAOException : Exception
    {
        public DAOException(string message) : base(message)
        { 

        }
    }
}
