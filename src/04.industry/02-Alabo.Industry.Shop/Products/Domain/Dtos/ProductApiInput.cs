using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Shop.Product.DiyModels {

    /// <summary>
    ///     商品列表
    /// </summary>
    public class ProductApiInput : ApiInputDto {

        /// <summary>
        ///     商品排序方式
        /// </summary>
        public ProductSortOrder SortOrder { get; set; }

        /// <summary>
        ///     搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        ///     最低价格
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        ///     最高价格
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// 价格区间
        /// 将PriceRang转换MinPrice,和MaxPrice
        /// </summary>
        public string PriceRang { get; set; }
        /// <summary>
        ///     商品分类Id，多个ID用逗号隔开
        /// </summary>
        public string ClassIds { get; set; }

        /// <summary>
        ///     商品标签ID，多个Id用逗号隔开
        /// </summary>
        public string TagIds { get; set; }

        /// <summary>
        ///     商品Id，比如浏览历史
        /// </summary>
        public string ProductIds { get; set; }

        /// <summary>
        ///     商品品牌Id
        /// </summary>
        public long? BrandId { get; set; }

        /// <summary>
        ///     商品模式
        /// </summary>
        public Guid? PriceStyleId { get; set; } = Guid.Parse("E0000000-1478-49BD-BFC7-E73A5D699000");

        /// <summary>
        ///     排序方式
        ///     0升序，1降序
        /// </summary>
        public int OrderType { get; set; } = 0;

        /// <summary>
        ///     总数量
        ///     如果为0，显示符合条件的
        /// 如果不为0，则显示具体的数量,不需要分页
        /// </summary>
        public long Count { get; set; } = 0;

        /// <summary>
        /// 库存: 小于这个库存的才显示
        /// </summary>
        public int Stock { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the price styles.
        ///     获取所有正常下面的商城模式
        /// </summary>
        /// <value>
        ///     The price styles.
        /// </value>
        [JsonIgnore]
        public IList<PriceStyleConfig> PriceStyles { get; set; } = new List<PriceStyleConfig>();

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        ///     是否为拼团商品
        /// </summary>
        public bool IsGroupBuy { get; set; } = true;


    }
}