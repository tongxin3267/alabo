using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.BranchCompanys.Domain.Entities
{
    /// <summary>
    /// 分公司
    /// </summary>

    [ClassProperty(Name = "分公司")]
    [BsonIgnoreExtraElements]
    [Table("People_BranchCompany")]
    [AutoDelete(IsAuto = true)]
    public class BranchCompany : UserTypeAggregateRoot<BranchCompany>
    {
    }
}