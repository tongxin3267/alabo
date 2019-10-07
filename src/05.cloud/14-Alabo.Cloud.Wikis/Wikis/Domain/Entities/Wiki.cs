using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_Wiki")]
    [ClassProperty(Name = "Wiki", Description = "Wiki")]
    public class Wiki : AggregateMongodbUserRoot<Wiki>
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public ObjectId FatherId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public ObjectId ProjectId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
    }
}