using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Open.Share.Domain.Entities;
using Alabo.App.Open.Share.Domain.Enums;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Open.Share.ViewModels {

    /// <summary>
    /// 分润配置
    /// </summary>
    [ClassProperty(Name = "分润配置", Icon = "fa fa-puzzle-piece", Description = "分润配置", ListApi = "Api/Reward/List",
        PageType = ViewPageType.List, PostApi = "Api/Reward/List")]
    [BsonIgnoreExtraElements]
    public class ViewReward : BaseViewModel {

        /// <summary>
        ///     分润ID ，主键自增
        /// </summary>
        [Required]
        [Display(Name = "分润ID")]
        public long Id { get; set; }

        /// <summary>
        ///     分润编号
        /// </summary>
        [Required]
        [Display(Name = "分润编号")]
        public string Serial { get; set; }

        /// <summary>
        ///     订单ID
        /// </summary>
        [Required]
        [Display(Name = "订单ID")]
        public long OrderId { get; set; }

        /// <summary>
        ///     订单编号
        /// </summary>
        [Required]
        [Display(Name = "订单编号")]
        public string OrderSerial { get; set; }

        /// <summary>
        ///     订单成交价格
        /// </summary>
        [Display(Name = "订单成交价格")]
        [Range(0, 99999999, ErrorMessage = "订单成交价格必须为大于等于0的数字")]
        public decimal OrderPrice { get; set; }

        /// <summary>
        ///     订单的用户ID
        /// </summary>
        [Required]
        [Display(Name = "订单的用户ID")]
        public long OrderUserId { get; set; }

        /// <summary>
        ///     订单交易分润时，用户昵称
        /// </summary>
        [Display(Name = "用户昵称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string OrderUserNick { get; set; }

        /// <summary>
        ///     获得分润的用户ID
        /// </summary>
        [Display(Name = "获得分润的用户ID")]
        public long UserId { get; set; }

        /// <summary>
        ///     分润获得者用户昵称
        /// </summary>
        [Display(Name = "用户昵称")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserNikeName { get; set; }

        /// <summary>
        ///     分润类型
        /// </summary>
        [Display(Name = "分润类型")]
        public string ModuleName { get; set; }

        /// <summary>
        ///     分润类型ID,从配置表中读取,
        /// </summary>
        [Display(Name = "分润类型ID")]
        public long FenRunTypeId { get; set; }

        /// <summary>
        ///     分润货币ID
        /// </summary>
        [Display(Name = "分润货币ID")]
        public Currency MoneyType { get; set; }

        /// <summary>
        ///     分润货币名称
        /// </summary>
        [Display(Name = "分润货币名称")]
        public string MoneyTypeName { get; set; }

        /// <summary>
        ///     分润金额
        /// </summary>
        [Display(Name = "分润金额")]
        [Range(0, 99999999, ErrorMessage = "分润金额必须为大于等于0的数字")]
        public decimal Price { get; set; }

        /// <summary>
        ///     分润后改账户金额
        /// </summary>
        [Display(Name = "分润后账户金额")]
        [Range(0, 99999999, ErrorMessage = "分润金额必须为大于等于0的数字")]
        public decimal AfterAcount { get; set; }

        /// <summary>
        ///     分润简要介绍
        /// </summary>
        [Display(Name = "分润简要介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     层次
        /// </summary>
        [Display(Name = "层次")]
        public long Level { get; set; }

        /// <summary>
        ///     分润费率
        /// </summary>
        [Display(Name = "分润费率")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     分润是否到账
        /// </summary>
        [Display(Name = "分润是否到账")]
        public bool IsAccount { get; set; }

        /// <summary>
        ///     分润状态。待确认TobeConfirm = 0,待对账TobeSettlement = 1,待分红TobeFenRun = 2,已成功Successful = 3,已取消Cancellation = 4,
        /// </summary>
        [Display(Name = "分润状态")]
        public FenRunStatus State { get; set; }

        /// <summary>
        ///     分润的时间，可以手动指定，为到账时候，可以通过此字段来处理
        /// </summary>
        [Display(Name = "分润的时间")]
        [DataType(DataType.Date)]
        public DateTime FenRunDate { get; set; } = DateTime.Now;

        /// <summary>
        ///     创建时间
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     用户分润确认时间
        /// </summary>
        [Display(Name = "用户分润确认时间")]
        [DataType(DataType.Date)]
        public DateTime ConfirmTime { get; set; } = DateTime.Now;

        ///// <summary>
        ///// 最后更新时间,格式:yyyy-MM-dd HH:mm:ss
        ///// </summary>
        //[Display(Name = "最后更新时间")]
        //[DataType(DataType.Date)]
        //public DateTime ModifiedTime { get; set; } = DateTime.Now;

        [Display(Name = "供应商名称")] public string StoreName { get; set; }

        /// <summary>
        ///     Reward
        /// </summary>
        public Reward Reward { get; set; }

        // static ViewReward() {
        //	MappingManager.CreateMapping<Domain.Entities.Reward, ViewReward>()
        //		.AddDefault()
        //		.Register();
        //	MappingManager.CreateMapping<ViewReward, Domain.Entities.Reward>()
        //		.AddDefault()
        //		.Register();
        //}
    }
}