using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 所有Webservice调用失败后返回序列的基本类型,包含了错误信息.
    /// </summary>
    public class WSResponseErrorBase : WSResponseBase
    {
        /// <summary>
        /// 错误的具体信息.
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的errorMessage如下
        /// <code>
        /// "errorMessage" : "参数错误"
        /// </code>
        /// </example>
        public string errorMessage { get; set; }
        internal WSResponseErrorBase(string errorMessage)
            : base(false)
        {
            this.errorMessage = errorMessage;
        }
    }
}
