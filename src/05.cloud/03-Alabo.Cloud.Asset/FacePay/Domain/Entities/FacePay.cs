using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Asset.FacePay.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("FacePay")]
    [ClassProperty(Name = "当面付记录", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class FacePay : AggregateMongodbUserRoot<FacePay>
    {
        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        ///     等级ID
        /// </summary>
        [Display(Name = "等级Id")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     等级
        /// </summary>
        [Display(Name = "等级")]
        public string GradeName { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal Amount { get; set; }
    }
}