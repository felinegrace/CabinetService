using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Framework.BusinessLayer
{
    public abstract class BOBase
    {
        protected BusinessContext context { get; private set; }

        protected BOBase(BusinessContext context)
        {
            this.context = context;
        }

        public abstract void handleBusiness();

        protected void validateParamCount(int count)
        {
            if(context.request.param.Count != count)
            {
                throw new BOException("invalid param count.");
            }
        }

        protected void validateParamAsSpecificType(int index, Type type)
        {
            if(!context.request.param.ElementAt<object>(index).GetType().Equals(type))
            {
                throw new BOException("invalid param type at " + index);
            }
        }

        protected void logOnValidatingParams()
        {
            Logger.debug("BusinessServer: validating params...");
        }

        protected void logOnLauchingDAO()
        {
            Logger.debug("BusinessServer: lauching DAO...");
        }

        protected void logOnFillingResult()
        {
            Logger.debug("BusinessServer: filling result...");
        }
        
    }
}
