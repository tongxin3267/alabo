﻿using Alabo.Datas.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alabo.Datas.Ef {

    /// <summary>
    ///     Ef工作单元扩展
    /// </summary>
    public static partial class Extensions {

        /// <summary>
        ///     清空缓存
        /// </summary>
        public static void ClearCache(this IUnitOfWork unitOfWork) {
            var dbContext = unitOfWork as DbContext;
            dbContext?.ChangeTracker.Entries().ToList().ForEach(entry => entry.State = EntityState.Detached);
        }
    }
}