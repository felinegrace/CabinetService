using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;

namespace Cabinet.Framework.BusinessLayer
{
    public abstract class BOBase
    {
        protected RawContext context { get; private set; }

        protected BOBase(RawContext context)
        {
            this.context = context;
        }

        public void handleBusiness()
        {
            processRequest();
            context.response.onResponsed();
        }

        protected abstract void processRequest();

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

        
    }
}
