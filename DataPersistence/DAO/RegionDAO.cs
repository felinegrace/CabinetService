using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet.Data.Persistence.DAO
{
    public class RegionDAO : CabinetTreeDAOBase
    {
        public int c(string name, string shortName)
        {
            CabTree_Region o = new CabTree_Region();
            o.name = name;
            o.shortName = shortName;
            regions.InsertOnSubmit(o);
            submit();
            return o.id;
        }

        public Table<CabTree_Region> r()
        {
            return regions;
        }

        public void u(int id, string name, string shortName)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.id == id);
            o.name = name;
            o.shortName = shortName;
            submit();
        }

        public void d(int id)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.id == id);
            regions.DeleteOnSubmit(o);
            submit();
        }

    }
}
