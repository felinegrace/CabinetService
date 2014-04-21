using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cabinet.Bridge.CommonEntity.CabinetTree
{
    public class Region : Jsonable
    {
        public int id {get; set;}
        public string name {get; set;}
        public string shortName {get; set;}
        public Region()
        {

        }
    }
}
