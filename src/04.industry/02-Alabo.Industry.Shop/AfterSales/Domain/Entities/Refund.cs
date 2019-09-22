using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Shop.AfterSale.Domain.Enums;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Entities
{

    /// <summary>
    ///     退款退货管理
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("AfterSale_Refund")]
    public class Refund : AggregateMongodbUserRoot<Refund>
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public long ProductId { get; set; }

        /// <summary>
        /// 店铺ID,该字段为冗余字段,方便以后查询
        /// </summary>
        [Required]
        public long StoreId { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [Required]
        public long OrderId { get; set; }
        /// <summary>
        /// 状态 1已发货 0 未发货
        /// </summary>
        public RefundStatus Status { get; set; }

        /// <summary>
        /// 原因 选择
        /// </summary>
        [Required]
        public string Reason { get; set; }

        /// <summary>
        /// 金额 包含运费,默认为商品金额
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 凭证,图片(路径 最多五张
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public RefundStatus Process { get; set; }

        /// <summary>
        /// 处理消息
        /// </summary>
        public string ProcessMessage { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

    }
}