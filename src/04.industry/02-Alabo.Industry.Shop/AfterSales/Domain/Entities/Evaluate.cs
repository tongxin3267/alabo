using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Shop.AfterSales.Domain.Entities
{
    /// <summary>
    /// 评价
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("AfterSale_Evaluate")]
    public class Evaluate : AggregateMongodbUserRoot<Evaluate>
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
        /// 评价内容
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        ///     评价方式，好评中评，差评
        /// </summary>
        public ReviewType ReviewType { get; set; }

        /// <summary>
        ///     商品评分，描述相符
        ///     最终的商品评分需更新到Product表中
        /// </summary>
        [Required]
        [Range(0, 5)]
        public long ProductScore { get; set; }

        /// <summary>
        ///     物流速度 物流评分
        /// </summary>
        [Required]
        [Range(0, 5)]
        public long LogisticsScore { get; set; }

        /// <summary>
        ///     服务评分
        /// </summary>
        [Required]
        [Range(0, 5)]
        public long ServiceScore { get; set; }

        /// <summary>
        /// 图片 逗号隔开(路径),最多五个
        /// </summary>
        public List<string> Images { get; set; }
    }
}
