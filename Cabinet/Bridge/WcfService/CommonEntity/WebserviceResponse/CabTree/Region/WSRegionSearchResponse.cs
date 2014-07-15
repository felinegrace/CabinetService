using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 方法 <see cref="Cabinet.Bridge.WcfService.ICabTreeService.regionSearch"/> 的返回值示例.
    /// </summary>
    public class WSRegionSearchResponse : WSResponseSuccessBase
    {
        /// <summary>
        /// 查询的区域的guid
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的guid如下
        /// <code>
        /// "regionGuid" : "68BFD8F7-F8BC-42DE-9F1F-B6B5C16436E3"
        /// </code>
        /// </example>
        public string regionGuid { get; set; }
        /// <summary>
        /// 查询的区域的名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的名称如下
        /// <code>
        /// "name" : "宁波电力公司"
        /// </code>
        /// </example>
        public string name { get; set; }
        /// <summary>
        /// 查询的区域的短名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的短名称如下
        /// <code>
        /// "shortName" : "nb"
        /// </code>
        /// </example>
        public string shortName { get; set; }
        internal WSRegionSearchResponse()
        {
            
        }
    }
}
