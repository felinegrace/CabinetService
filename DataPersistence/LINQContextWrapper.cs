using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Utility;

namespace Cabinet.DataPersistence
{
    class LINQContextWrapper
    {
        public CabinetDatabaseDataContext context { get; private set; }

        public LINQContextWrapper()
        {
            context = new ();
            context.Log = new Logger4LINQ();
        }

    }
}
