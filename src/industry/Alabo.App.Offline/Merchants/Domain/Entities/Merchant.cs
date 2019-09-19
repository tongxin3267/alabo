using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Offline.Merchants.Domain.Entities {

    /// <summary>
    /// 商家
    /// </summary>
    [Table("Offline_Merchant")]
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "商家")]
    public class Merchant : AggregateMongodbRoot<Merchant> {

        /// <summary>
        /// 商家名称
        /// </summary>
        [Display(Name = "商家名称")]
        public string Name { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; }

        /// <summary>
        /// 推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人")]
        public long ParentUserId { get; set; }

        /// <summary>
        /// 用户等级Id
        /// 每个类型对应一个等级
        /// </summary>
        [Display(Name = "等级Id")]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;
    }
}