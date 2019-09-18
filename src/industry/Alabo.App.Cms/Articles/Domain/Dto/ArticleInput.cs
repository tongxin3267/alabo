using Alabo.App.Cms.Articles.Domain.Enums;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.Domains.Enums;

namespace Alabo.App.Cms.Articles.Domain.Dto {

    /// <summary>
    ///     Class ArticleInput.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Query.Dto.PagedInputDto" />
    public class ArticleInput : ListInput {

        /// <summary>
        ///     频道Id
        /// </summary>
        public string ChannelId { get; set; } = string.Empty;

        /// <summary>
        ///     搜索关键字
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        /// <summary>
        ///     商品分类Id，多个ID用逗号隔开
        /// </summary>
        public string RelationIds { get; set; } = string.Empty;

        /// <summary>
        ///     商品标签ID，多个Id用逗号隔开
        /// </summary>
        public string TagIds { get; set; } = string.Empty;

        /// <summary>
        ///     排序方式
        ///     0升序，1降序
        /// </summary>
        public int OrderType { get; set; } = 0;

        /// <summary>
        ///     总数量
        ///     如果为0，显示符合条件的
        /// </summary>
        public long TotalCount { get; set; } = 0;

        public Status Status { get; set; }

        public ArticleSortOrder SortOrder { get; set; }
    }
}