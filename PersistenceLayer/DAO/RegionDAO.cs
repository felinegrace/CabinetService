using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.PersistenceLayer
{
    public class RegionDAO : DAOBase
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

        public IEnumerable<RegionVO> r()
        {
            var q = from o in regions
                    select new RegionVO
                    {
                        id = o.id,
                        name = o.name,
                        shortName = o.shortName
                    };
            return q;
        }

        public void u(RegionVO p)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.id == p.id);
            if(o == null)
            {
                throw new DAOException("RegionDAO u: no such item , id = " + p.id);
            }
            o.name = p.name;
            o.shortName = p.shortName;
            submit();
        }

        public void d(int id)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.id == id);
            if (o == null)
            {
                throw new DAOException("RegionDAO d: no such item , id = " + id);
            }
            regions.DeleteOnSubmit(o);
            submit();
        }

    }
}
