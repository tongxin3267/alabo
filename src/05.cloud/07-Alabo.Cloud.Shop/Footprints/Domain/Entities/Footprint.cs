using Alabo.Cloud.Shop.Footprints.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Shop.Footprints.Domain.Entities
{
    /// <summary>
    ///     通用足迹
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Attach_FootPrint")]
    [ClassProperty(Name = "足迹")]
    public class Footprint : AggregateMongodbUserRoot<Footprint>
    {
        /// <summary>
        ///     评论类型
        ///     比如订单、商品、文章等评论
        /// </summary>
        [Display(Name = "评论类型")]
        public FootprintType Type { get; set; }

        /// <summary>
        ///     对应的实体Id
        /// </summary>
        [Display(Name = "对应的实体Id")]
        public string EntityId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     链接地址
        /// </summary>
        public string Url { get; set; }
    }
}