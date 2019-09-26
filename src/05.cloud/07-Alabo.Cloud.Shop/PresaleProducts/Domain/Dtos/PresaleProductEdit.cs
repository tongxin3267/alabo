namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos
{
    public class PresaleProductEdit
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 产品SkuId
        /// </summary>
        public long? SkuId { get; set; }

        /// <summary>
        /// 虚拟货币价格
        /// </summary>
        public decimal VirtualPrice { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public long Sort { get; set; }
    }
}
