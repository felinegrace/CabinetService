using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.DataPersistence;
using Cabinet.Utility;

namespace TestDataPersistence
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            RegionImpl e = new RegionImpl();
        }
    }
}
