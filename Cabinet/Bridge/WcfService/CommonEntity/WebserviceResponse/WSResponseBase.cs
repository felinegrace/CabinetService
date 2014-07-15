using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cabinet.Framework.CommonEntity;
using Cabinet.Utility;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 所有Webservice返回序列的基本类型,包含了指代成功或者失败的值.
    /// </summary>
    public class WSResponseBase : Jsonable
    {
        /// <summary>
        /// 本次调用的结果,以bool表示
        /// </summary>
        /// <value>
        /// 成功为 true, 失败为 false.
        /// </value>
        /// <example> 
        /// 以json表示的成功如下
        /// <code>
        /// "isSuccess" : true
        /// </code>
        /// </example>
        public bool isSuccess { get; set; }

        internal WSResponseBase(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }
    }
}
