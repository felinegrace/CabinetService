using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 方法 <see cref="Cabinet.Bridge.WcfService.ICabTreeService.cabinetSearch"/> 的返回值示例.
    /// </summary>
    public class WSCabinetSearchResponse : WSResponseSuccessBase
    {
        /// <summary>
        /// 查询的屏柜的guid
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的guid如下
        /// <code>
        /// "cabinetGuid" : "68BFD8F7-F8BC-42DE-9F1F-B6B5C16436E3"
        /// </code>
        /// </example>
        public string cabinetGuid { get; set; }
        /// <summary>
        /// 查询的屏柜的名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的名称如下
        /// <code>
        /// "name" : "Ca-001"
        /// </code>
        /// </example>
        public string name { get; set; }

        /// <summary>
        /// 查询的屏柜的名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的名称如下
        /// <code>
        /// "name" : "Ca-001"
        /// </code>
        /// </example>
        int height;

        /// <summary>
        /// 查询的屏柜的名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的名称如下
        /// <code>
        /// "name" : ""
        /// </code>
        /// </example>
        int width;

        /// <summary>
        /// 查询的屏柜的名称
        /// </summary>
        /// <example> 
        /// 以json的字符串表示的名称如下
        /// <code>
        /// "depth" : 
        /// </code>
        /// </example>
        int depth;
        internal WSCabinetSearchResponse()
        {
            
        }
    }
}
