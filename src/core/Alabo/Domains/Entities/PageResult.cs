using System.Collections.Generic;

namespace Alabo.Domains.Entities
{
    /// <summary>
    ///     分页数据输出
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        ///     当前页记录数
        /// </summary>
        public long CurrentSize { get; set; }

        /// <summary>
        ///     总记录数 private
        /// </summary>
        public long RecordCount { get; set; }

        /// <summary>
        ///     当前页 private
        /// </summary>
        public long PageIndex { get; set; }

        /// <summary>
        ///     每页记录数 private
        /// </summary>

        public long PageSize { get; set; }

        /// <summary>
        ///     总页数
        /// </summary>
        public long PageCount { get; set; }

        /// <summary>
        ///     分页数据
        /// </summary>
        public List<T> Result { get; set; }

        /// <summary>
        ///     转换成Api结果，前台分页使用PagedResult
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static PageResult<T> Convert(PagedList<T> result)
        {
            if (result == null) {
                return null;
            }

            var apiRusult = new PageResult<T>
            {
                PageCount = result.PageCount,
                Result = result,
                RecordCount = result.RecordCount,
                CurrentSize = result.CurrentSize,
                PageIndex = result.PageIndex,
                PageSize = result.PageSize
            };
            return apiRusult;
        }
    }
}