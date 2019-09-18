namespace Alabo.App.Shop.Activitys.Modules.ProductNumberLimit.Dtos {

    /// <summary>
    ///     用户购买商品数量
    /// </summary>
    public class UserProductCount {
        public long UserId { get; set; }

        public long ProductId { get; set; }

        public long Count { get; set; }
    }
}