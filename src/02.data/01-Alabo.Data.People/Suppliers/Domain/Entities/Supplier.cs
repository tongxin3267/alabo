using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Suppliers.Domain.Entities
{
    [ClassProperty(Name = "供应商")]
    [BsonIgnoreExtraElements]
    [Table("People_Supplier")]
    [AutoDelete(IsAuto = true)]
    public class Supplier : UserTypeAggregateRoot<Supplier>
    {
    }
}