using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.FacePay.Domain.Entities {

    [BsonIgnoreExtraElements]
    [Table("FacePay")]
    [ClassProperty(Name = "当面付记录", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class FacePay : AggregateMongodbUserRoot<FacePay> {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 等级ID
        /// </summary>
        [Display(Name = "等级Id")]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [Display(Name = "等级")]
        public string GradeName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        public decimal Amount { get; set; }
    }
}