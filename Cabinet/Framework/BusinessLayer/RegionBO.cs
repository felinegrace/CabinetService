using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Framework.PersistenceLayer;

namespace Cabinet.Framework.BusinessLayer
{
    public class RegionBO : BOBase
    {
        private RegionDAO regionDao { get; set; }
        public RegionBO(BusinessContext context) : base(context)
        {
            regionDao = new RegionDAO();
        }

        protected override void processRequest()
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
            validateParamCount(2);
            validateParamAsSpecificType(0,typeof(string));
            string name = context.request.param.ElementAt<object>(0) as string;
            validateParamAsSpecificType(1,typeof(string));
            string shortName = context.request.param.ElementAt<object>(1) as string;
            int id = regionDao.c(name, shortName);
            context.response.result.Add(id);
        }
    }
}
