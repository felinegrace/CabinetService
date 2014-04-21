using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Bridge.CommonEntity.CabinetTree;
using Cabinet.Utility;

namespace TestEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            Region r = new Region();
            r.id = 1;
            r.name = "2";
            r.shortName = "3";
            Logger.debug("{0}",r.toJson());

        }
    }
}
