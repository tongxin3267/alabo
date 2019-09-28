using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Suppliers.Domain.Entities
{
    /// <summary>
    /// 供应商
    /// </summary>
    [ClassProperty(Name = "供应商")]
    [BsonIgnoreExtraElements]
    [Table("People_Supplier")]
    [AutoDelete(IsAuto = true)]
    public class Supplier : UserTypeAggregateRoot<Supplier>
    {
    }
}