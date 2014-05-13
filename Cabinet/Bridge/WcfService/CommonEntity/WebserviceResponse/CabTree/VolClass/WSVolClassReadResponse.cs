﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cabinet.Bridge.WcfService.CommonEntity
{
    /// <summary>
    /// 方法 <see cref="Cabinet.Bridge.WcfService.ICabTreeService.volClassRead"/> 的返回值示例.
    /// </summary>
    public class WSVolClassReadResponse : WSResponseSuccessBase
    {
        /// <summary>
        /// 读取的电压等级的guid列表, 注意: 不同区域下guid并不相同
        /// </summary>
        /// <example> 
        /// 以json的字符串数组表示的guid如下
        /// <code>
        /// "volClassGuidList" : [
        ///     "68BFD8F7-F8BC-42DE-9F1F-B6B5C16436E3",
        ///     "5926252C-00D0-4A29-AEF6-D8302EBCD435"
        /// ]    
        /// </code>
        /// </example>
        public List<string> volClassGuidList { get; set; }
        internal WSVolClassReadResponse()
        {
            
        }
    }
}
