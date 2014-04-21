using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.DataPersistence.ContextWrapper;

namespace Cabinet.DataPersistence
{
    public class RegionImpl
    {
        public RegionImpl()
        {
            CabinetTreeContextWrapper w = new CabinetTreeContextWrapper();
            w.context.CabTree_Regions.InsertOnSubmit(new Cabinet.DataPersistence.Entity.Region("c", "d"));
            w.context.SubmitChanges();
        }


    }
}
