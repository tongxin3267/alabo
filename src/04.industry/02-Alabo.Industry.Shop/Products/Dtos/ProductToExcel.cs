namespace Alabo.Industry.Shop.Products.Dtos
{
    public class ProductToExcel
    {
        public string RelationName { get; set; }

        public string ProductName { get; set; }

        public string Bn { get; set; }

        /// <summary>
        ///     市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        ///     标准价
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        ///     众享价
        /// </summary>
        public decimal PremiumPrice { get; set; }

        /// <summary>
        /// </summary>
        public decimal UltimatePrice { get; set; }

        /// <summary>
        /// </summary>
        public decimal MajordomoPrice { get; set; }

        /// <summary>
        ///     院长价
        /// </summary>
        public decimal DeanPrice { get; set; }

        /// <summary>
        ///     营销中心价
        /// </summary>
        public decimal CentrePrice { get; set; }

        /// <summary>
        ///     准营销中心价
        /// </summary>
        public decimal ReadyCentrePrice { get; set; }

        public long Stock { get; set; }

        public string TotalSalesVolume { get; set; }

        public string Status { get; set; }
    }
}