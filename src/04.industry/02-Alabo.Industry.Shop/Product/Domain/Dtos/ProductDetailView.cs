using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Shop.Product.Domain.Dtos
{
    /// <summary>
    /// 商品详情ViewModel
    /// </summary>
    public class ProductDetailView : Entities.Product
    {
        /// <summary>
        /// 是否显示价格
        /// </summary>
        public bool IsFrontShowPrice { get; set; }

        /// <summary>
        /// 价格替代文本
        /// </summary>
        public string PriceAlterText { get; set; }
    }
}
