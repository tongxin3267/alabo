namespace Alabo.Domains.Entities.Core {

    /// <summary>
    ///     排序
    /// </summary>
    public interface IPagedList {

        /// <summary>
        ///     数据量
        /// </summary>
        long RecordCount { get; }

        /// <summary>
        ///     当前所在页码
        /// </summary>
        long PageIndex { get; }

        /// <summary>
        ///     分页大小
        /// </summary>
        long PageSize { get; }

        /// <summary>
        ///     总页数
        /// </summary>
        long PageCount { get; }

        /// <summary>
        ///     排序条件
        /// </summary>
        string Order { get; set; }
    }
}