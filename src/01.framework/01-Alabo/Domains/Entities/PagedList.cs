using Alabo.Domains.Entities.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Domains.Entities {

    public class PagedList<T> : List<T>, IPagedList {

        /// <summary>
        /// </summary>
        public PagedList() {
        }

        private PagedList(IEnumerable<T> source)
            : base(source) {
        }

        /// <summary>
        ///     当前页记录数
        /// </summary>
        [JsonProperty(PropertyName = "currentsize")]
        public long CurrentSize { get; set; }

        /// <summary>
        ///     分页数据
        /// </summary>
        public IEnumerable<T> Result { get; set; }

        /// <summary>
        ///     总记录数 private
        /// </summary>
        [JsonProperty(PropertyName = "recordcount")]
        public long RecordCount { get; set; }

        /// <summary>
        ///     当前页 private
        /// </summary>
        [JsonProperty(PropertyName = "pageindex")]
        public long PageIndex { get; set; }

        /// <summary>
        ///     每页记录数 private
        /// </summary>
        [JsonProperty(PropertyName = "pagesize")]
        public long PageSize { get; set; }

        /// <summary>
        ///     总页数
        /// </summary>
        [JsonProperty(PropertyName = "pagecount")]
        public long PageCount {
            get {
                if (PageSize == 0) {
                    return 0;
                }

                return RecordCount % PageSize == 0 ? RecordCount / PageSize : RecordCount / PageSize + 1;
            }
        }

        /// <summary>
        ///     排序条件
        /// </summary>
        public string Order { get; set; }

        public static PagedList<T> Create(IEnumerable<T> source, long recordCount, long pageSize, long pageIndex) {
            var res = new PagedList<T>(source) {
                CurrentSize = source.Count(),
                Result = source,
                RecordCount = recordCount,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
            return res;
        }
    }
}