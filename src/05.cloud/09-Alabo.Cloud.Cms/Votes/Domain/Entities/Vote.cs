using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Cms.Votes.Domain.Entities {

    /// <summary>
    /// 投票
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Market_Vote")]
    public class Vote : AggregateMongodbUserRoot<Vote> {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}