﻿using Alabo.Domains.Repositories.Pager;

namespace Alabo.Datas.Queries {

    /// <summary>
    ///     查询参数
    /// </summary>
    public class QueryParameter : Pager, IQueryParameter {

        /// <summary>
        ///     搜索关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}