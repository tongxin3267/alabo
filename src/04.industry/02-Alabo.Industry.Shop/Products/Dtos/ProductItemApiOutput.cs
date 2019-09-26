using System.Collections.Generic;
using Alabo.Domains.Query.Dto;
using Alabo.Industry.Shop.Products.ViewModels;

namespace Alabo.Industry.Shop.Products.Dtos {

    public class ProductItemApiOutput : ApiInputDto
    {

        /// <summary>
        ///     样式格式，不通的数据可能显示不同的格式
        /// </summary>
        public int StyleType { get; set; } = 1;

        /// <summary>
        ///     返回数据源的总页数
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// 是否显示价格
        /// </summary>
        public bool  IsFrontShowPrice { get; set; }

        /// <summary>
        /// 价格替代文本
        /// </summary>
        public string PriceAlterText { get; set; }

        /// <summary>
        /// </summary>
        public IList<ProductItem> ProductItems { get; set; }
    }
}