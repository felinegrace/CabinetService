using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 所有Webservice调用成功后返回序列的基本类型,详见各继承类型.
    /// </summary>
    public class WSResponseSuccessBase : WSResponseBase
    {
        internal WSResponseSuccessBase() : base(true)
        {

        }
    }
}
