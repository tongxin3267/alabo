using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Article.src.Domains.Enums {
	/// <summary>
	/// 文章发布状态
	/// </summary>
	public enum ArticleStates {
		/// <summary>
		/// 未发布
		/// </summary>
		UnPublish = 0,
		/// <summary>
		/// 已发布
		/// </summary>
		Publish = 1,
        /// <summary>
        /// 已删除
        /// </summary>
        Delete=2
	}
}
