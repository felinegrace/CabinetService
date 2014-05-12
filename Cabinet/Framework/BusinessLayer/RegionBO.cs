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
                case ("create"):
                    doCreate();
                    break;
                default:
                    break;
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
    }
}
