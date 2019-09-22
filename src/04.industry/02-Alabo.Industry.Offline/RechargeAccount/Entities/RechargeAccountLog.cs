using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants.Domain.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Offline.RechargeAccount.Entities
{
    [BsonIgnoreExtraElements]
    [Table("RechargeAccountLog")]
    [ClassProperty(Name = "储值记录", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class RechargeAccountLog : AggregateMongodbUserRoot<RechargeAccountLog>
    {
        /// <summary>
        /// 储值金额
        /// </summary>
        [BsonRequired]
        public decimal StoreAmount { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        [BsonRequired]
        public decimal ArriveAmount { get; set; }

        /// <summary>
        /// 赠送兑换券
        /// </summary>
        [BsonRequired]
        public decimal GiveChangeAmount { get; set; }

        /// <summary>
        /// 赠送购物券
        /// </summary>
        [BsonRequired]
        public decimal GiveBuyAmount { get; set; }

        /// <summary>
        /// 赠送打折券
        /// </summary>
        [BsonRequired]
        public decimal DiscountAmount { get; set; }
    }
}
