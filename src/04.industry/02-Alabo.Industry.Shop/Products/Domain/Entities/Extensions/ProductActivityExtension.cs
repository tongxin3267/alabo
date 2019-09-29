using System.Collections.Generic;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Products.Domain.Entities.Extensions
{
    /// <summary>
    ///     活动扩展
    /// </summary>
    public class ProductActivityExtension : EntityExtension
    {
        /// <summary>
        ///     IsGroupBuy
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        ///     activities
        /// </summary>
        public IList<ProductActivity> Activitys { get; set; } = new List<ProductActivity>();

        /// <summary>
        ///     user permission
        /// </summary>
        public UserPermissions UserPermissions { get; set; }

        /// <summary>
        ///     UserGradePrice
        /// </summary>
        public List<UserGradePriceView> UserGradePrices { get; set; }
    }

    /// <summary>
    ///     Class ProductActivity.
    /// </summary>
    public class ProductActivity
    {
        /// <summary>
        ///     Key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     所属营销活动类型，如：Alabo.App.Shop.Activitys.Domain.Modules.PinTuanActivity
        ///     比如满就送，或者限量购
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     具体活动内容，活动类型的Json数据
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    ///     UserPermissions
    /// </summary>
    public class UserPermissions
    {
        /// <summary>
        ///     单次最多购买
        /// </summary>
        public long SingleBuyCountMax { get; set; }

        /// <summary>
        ///     单次最低购买
        /// </summary>
        public long SingleBuyCountMin { get; set; }

        /// <summary>
        ///     用户购买此商品数量限制
        /// </summary>
        public long TotalBuyCountMax { get; set; }

        /// <summary>
        ///     have lever to view
        /// </summary>
        public bool IsMemberLeverView { get; set; } = true;

        /// <summary>
        ///     have lever to buy
        /// </summary>
        public bool IsMemberLeverBuy { get; set; } = true;
    }

    /// <summary>
    ///     UserGradePriceView
    /// </summary>
    public class UserGradePriceView
    {
        /// <summary>
        ///     name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     low price
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        ///     high price
        /// </summary>
        public decimal HighPrice { get; set; }
    }
}