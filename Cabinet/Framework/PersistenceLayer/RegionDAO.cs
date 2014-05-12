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
        public RegionVO s(Guid guid)
        {
            var q = from o in regions
                    where o.guid == guid
                    select new RegionVO
                    {
                        id = o.id,
                        guid = o.guid,
                        name = o.name,
                        shortName = o.shortName
                    };
            return q.Single<RegionVO>();
        }

        public void c(Guid guid, string name, string shortName)
        {
            CabTree_Region o = new CabTree_Region();
            o.guid = guid;
            o.name = name;
            o.shortName = shortName;
            regions.InsertOnSubmit(o);
            submit();
        }

        public IEnumerable<RegionVO> r()
        {
            var q = from o in regions
                    select new RegionVO
                    {
                        id = o.id,
                        guid = o.guid,
                        name = o.name,
                        shortName = o.shortName
                    };
            return q;
        }

        public void u(RegionVO p)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.guid == p.guid);
            if(o == null)
            {
                throw new DAOException("RegionDAO u: no such item , guid = " + p.guid);
            }
            o.name = p.name;
            o.shortName = p.shortName;
            submit();
        }

        public void d(Guid guid)
        {
            CabTree_Region o = regions.Single<CabTree_Region>(q => q.guid == guid);
            if (o == null)
            {
                throw new DAOException("RegionDAO d: no such item , guid = " + guid);
            }
            regions.DeleteOnSubmit(o);
            submit();
        }

    }
}
