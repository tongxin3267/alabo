using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Deliveries.Domain.Entities.Extensions {

    /// <summary>
    ///     店铺评分
    /// </summary>
    public class StoreScore : EntityExtension {

        /// <summary>
        ///     人气
        /// </summary>
        public string Popularity { get; set; }

        /// <summary>
        ///     综合评分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        ///     商品得分
        /// </summary>
        public decimal ProductScore { get; set; }

        /// <summary>
        ///     服务得分
        /// </summary>
        public decimal ServiceScore { get; set; }

        /// <summary>
        ///     物流得分
        /// </summary>
        public decimal LogisticsScore { get; set; }
    }
}