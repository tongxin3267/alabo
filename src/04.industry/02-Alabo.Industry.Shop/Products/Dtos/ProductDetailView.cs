using Alabo.Industry.Shop.Products.Domain.Entities;

namespace Alabo.Industry.Shop.Products.Dtos
{
    /// <summary>
    /// 商品详情ViewModel
    /// </summary>
    public class ProductDetailView : Product
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
