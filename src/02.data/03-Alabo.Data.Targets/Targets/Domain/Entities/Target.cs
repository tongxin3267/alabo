using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Entities
{
    /// <summary>
    ///     目标事务
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Target_Target")]
    [ClassProperty(Name = "目标", Description = "目标")]
    public class Target : AggregateMongodbUserRoot<Target>
    {
        /// <summary>
        /// 迭代Id
        /// </summary>
        public ObjectId IterationId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 具体内容
        /// </summary>
        public string Content { get; set; }
    }
}