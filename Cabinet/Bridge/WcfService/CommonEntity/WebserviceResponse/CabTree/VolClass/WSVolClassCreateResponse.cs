using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 方法 <see cref="Cabinet.Bridge.WcfService.ICabTreeService.volClassCreate"/> 的返回值示例.
    /// </summary>
    public class WSVolClassCreateResponse : WSResponseSuccessBase
    {
        /// <summary>
        /// 生成的电压等级的guid, 注意: 不同区域下guid并不相同
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的guid如下
        /// <code>
        /// "volClassGuid" : "68BFD8F7-F8BC-42DE-9F1F-B6B5C16436E3"
        /// </code>
        /// </example>
        public string volClassGuid { get; set; }
        internal WSVolClassCreateResponse()
        {
            
        }
    }
}
