using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Share.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("Share_RewardConfig")]
    [ClassProperty(Name = "分润配置", Icon = "fa fa-cog", SortOrder = 1)]
    public class RewardRule : AggregateMongodbUserRoot<RewardRule> {
    }
}