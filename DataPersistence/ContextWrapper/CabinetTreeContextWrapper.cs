using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Utility;

namespace Cabinet.DataPersistence.ContextWrapper
{
    class CabinetTreeContextWrapper
    {
        public CabinetTreeDataContext context { get; private set; }
        public CabinetTreeContextWrapper()
        {
            context = new CabinetTreeDataContext();
            context.Log = new Logger4LINQ();
        }
    }
}
