﻿using Alabo.Domains.Entities;
using System.Collections.Generic;

namespace Alabo.UI.Design.AutoTables {

    /// <summary>
    ///     自动表单，可以构建前台自动列表
    /// </summary>
    public interface IAutoTable {

        /// <summary>
        ///     操作链接
        /// </summary>
        /// <returns></returns>
        List<TableAction> Actions();
    }

    /// <summary>
    ///     自动表单，可以构建前台自动列表
    /// </summary>
    public interface IAutoTable<T> : IAutoTable {

        /// <summary>
        ///     通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        PageResult<T> PageTable(object query, AutoBaseModel autoModel);
    }
}