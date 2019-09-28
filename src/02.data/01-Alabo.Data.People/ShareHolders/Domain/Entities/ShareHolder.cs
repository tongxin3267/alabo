using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.ShareHolders.Domain.Entities
{
    /// <summary>
    /// 股东
    /// </summary>
    [ClassProperty(Name = "股东")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_ShareHolder")]
    public class ShareHolder : UserTypeAggregateRoot<ShareHolder>
    {
    }
}