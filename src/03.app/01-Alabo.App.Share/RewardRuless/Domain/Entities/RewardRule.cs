using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Share.RewardRuless.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("Share_RewardConfig")]
    [ClassProperty(Name = "分润配置", Icon = "fa fa-cog", SortOrder = 1)]
    public class RewardRule : AggregateMongodbUserRoot<RewardRule>
    {
    }
}