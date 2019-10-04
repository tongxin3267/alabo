using Alabo.Domains.Entities;
using System;

namespace Alabo.UI.Design.AutoLists {

    /// <summary>
    ///     自动列表接口
    ///     对应移动端zk-list
    /// </summary>
    public interface IAutoList {

        /// <summary>
        ///     通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel);

        /// <summary>
        ///     构建zk-list搜索和标签功能
        ///     一般为数据库实体
        /// </summary>
        /// <returns></returns>
        Type SearchType();
    }
}