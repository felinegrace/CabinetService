using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Cabinet.Bridge.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CabTreeService" in both code and config file together.
    public class CabTreeService : ICabTreeService
    {
        public int create(string name, string shortName)
        {
            return new RegionBusinessService().create(name, shortName);
        }
    }
}
