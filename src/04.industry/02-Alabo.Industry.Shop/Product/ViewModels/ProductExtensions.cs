using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.ViewModels {

    /// <summary>
    ///     商品所有属性，包括商品参数和商品销售属性
    /// </summary>
    public class ProductExtensions : BaseViewModel {

        /// <summary>
        ///     类目,商品的类目数据 类目数据保存到数据库里
        /// </summary>
        public Category.Domain.Entities.Category ProductCategory { get; set; }

        /// <summary>
        ///     商品缩略图列表(非数据库字段)
        /// </summary>
        public List<ProductThum> ProductThums { get; set; }

        /// <summary>
        ///     商品相册(非数据库字段)
        /// </summary>
        [Display(Name = "商品相册")]
        public string Images { get; set; }

        /// <summary>
        ///     商品Sku列表
        /// </summary>

        public IList<ProductSku> ProductSkus { get; set; }

        /// <summary>
        ///     商城模式
        /// </summary>
        public PriceStyleConfig PriceStyleConfig { get; set; }

        /// <summary>
        ///     商品所属店铺
        /// </summary>
        public Store.Domain.Entities.Store Store { get; set; }
    }
}