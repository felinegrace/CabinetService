using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.PersistenceLayer;
using Cabinet.Utility;

namespace Cabinet.Framework.BusinessLayer
{
    public class RegionBO : BOBase
    {
        private RegionDAO regionDao { get; set; }
        public RegionBO(BusinessContext context) : base(context)
        {
            regionDao = new RegionDAO();
        }

        public override void handleBusiness()
        {
            switch(context.request.method)
            {
                case ("search"):
                    doSearch();
                    break;
                case ("create"):
                    doCreate();
                    break;
                case ("read"):
                    doRead();
                    break;
                case ("update"):
                    doUpdate();
                    break;
                case ("delete"):
                    doDelete();
                    break;
                default:
                    break;
            }
        }

        private void doSearch()
        {
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid guid = (Guid)context.request.param.ElementAt<object>(0);
            logOnLauchingDAO();
            var q = from o in regionDao.r()
                    where o.guid == guid
                    select o;
            
            if(q.Count<RegionVO>() == 1)
            {
                RegionVO vo = q.Single<RegionVO>();
                logOnFillingResult();
                context.response.result.Add(vo.guid);
                context.response.result.Add(vo.name);
                context.response.result.Add(vo.shortName);
            }
            else
            {
                throw new BOException("no such region of guid :" + guid.ToString());
            }
        }

        private void doCreate()
        {
            logOnValidatingParams();
            validateParamCount(2);
            Guid guid = Guid.NewGuid();
            validateParamAsSpecificType(0, typeof(string));
            string name = context.request.param.ElementAt<object>(0) as string;
            validateParamAsSpecificType(1, typeof(string));
            string shortName = context.request.param.ElementAt<object>(1) as string;
            logOnLauchingDAO();
            regionDao.c(guid, name, shortName);
            logOnFillingResult();
            context.response.result.Add(guid);
        }

        private void doRead()
        {
            logOnValidatingParams();
            validateParamCount(0);
            logOnLauchingDAO();
            var q = from o in regionDao.r()
                    select o.guid.ToString();
            List<string> regionList = q.ToList<string>();
            logOnFillingResult();
            context.response.result.Add(regionList);
        }

        private void doUpdate()
        {
            logOnValidatingParams();
            validateParamCount(3);
            RegionVO regionVO = new RegionVO();
            validateParamAsSpecificType(0, typeof(Guid));
            regionVO.guid = (Guid)context.request.param.ElementAt<object>(0);
            validateParamAsSpecificType(1, typeof(string));
            regionVO.name = context.request.param.ElementAt<object>(1) as string;
            validateParamAsSpecificType(2, typeof(string));
            regionVO.shortName = context.request.param.ElementAt<object>(2) as string;
            logOnLauchingDAO();
            regionDao.u(regionVO);
            logOnFillingResult();
        }

        private void doDelete()
        {
            logOnValidatingParams();
            validateParamCount(1);
            validateParamAsSpecificType(0, typeof(Guid));
            Guid guid = (Guid)context.request.param.ElementAt<object>(0);
            logOnLauchingDAO();
            regionDao.d(guid);
            logOnFillingResult();
        }
    }
}
