using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Article.src.Entities
{
    public class AjaxResult
    {
        /// <summary>
        /// 操作状态(true 表示成功，false 表示失败)
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
    }
}
