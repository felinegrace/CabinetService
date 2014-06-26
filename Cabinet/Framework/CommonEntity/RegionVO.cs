using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
{
    public class RegionVO : Jsonable
    {
        public int id {get; set;}
        public Guid guid { get; set; }
        public string name {get; set;}
        public string shortName {get; set;}
    }
}
