using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Alabo.Cloud.Wikis.Settings.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_WikiClass")]
    [ClassProperty(Name = "Wiki分类", Description = "Wiki")]
    public class WikiClass : AggregateMongodbUserRoot<WikiClass>
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级分类Id
        /// </summary>
        public ObjectId FatherId { get; set; }
    }
}