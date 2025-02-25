﻿using Alabo.Domains.Repositories.Pager;
using Alabo.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Alabo.Datas.Ef {

    /// <summary>
    ///     查询扩展
    /// </summary>
    public static partial class Extensions {

        /// <summary>
        ///     转换为分页列表，包含排序分页操作
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static async Task<PagerList<TEntity>> ToPagerListAsync<TEntity>(this IQueryable<TEntity> source,
            IPager pager) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            if (pager == null) {
                throw new ArgumentNullException(nameof(pager));
            }

            return new PagerList<TEntity>(pager, await source.Page(pager).ToListAsync());
        }
    }
}