using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Attach.Domain.Entities {

    /// <summary>
    ///     通用评论表
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Attach_Comment")]
    [ClassProperty(Name = "通用评论表")]
    public class Comment : AggregateMongodbUserRoot<Comment> {

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