using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Perset.src.Enum
{
    /// <summary>
    /// 评论审核
    /// </summary>
    public enum CommentCheck
    {
        /// <summary>
        /// 未审核 
        /// </summary>
        UnCheck = 0,
        /// <summary>
        /// 已审核 
        /// </summary>
        Checked = 1,
        /// <summary>
        /// 已回复 
        /// </summary>
        Replyed = 2,
        /// <summary>
        /// 已作废 
        /// </summary>
        Cancelled = 3,
    }
}
