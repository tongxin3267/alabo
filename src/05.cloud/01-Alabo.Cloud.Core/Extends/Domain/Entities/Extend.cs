using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace _01_Alabo.Cloud.Core.Extends.Domain.Entities
{
    /// <summary>
    ///     扩展属性
    ///     TODO 所有字段的扩展属性
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Attach_Comment")]
    [ClassProperty(Name = "扩展属性")]
    public class Extend : AggregateMongodbUserRoot<Extend>
    {
        /// <summary>
        ///     评论类型
        ///     比如订单、商品、文章等评论
        /// </summary>
        [Display(Name = "评论类型")]
        public string Type { get; set; }

        /// <summary>
        ///     对应的实体Id
        /// </summary>
        [Display(Name = "对应的实体Id")]
        public object EntityId { get; set; }
    }
}