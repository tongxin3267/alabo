using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities
{
    /// <summary>
    ///     分润订单统计
    /// </summary>
    [ClassProperty(Name = "分润订单统计", Icon = "fa fa-file", Description = "分润订单统计")]
    [BsonIgnoreExtraElements]
    [Table("Things_ShareOrderReport")]
    public class ShareOrderReport : AggregateMongodbUserRoot<ShareOrderReport>
    {
        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, GroupTabId = 1, EditShow = true, ListShow = false, Width = "50",
            Link = "/Admin/Reward/List?OrderId=[[ShareOrderId]]")]
        [Display(Name = "分享订单")]
        public long ShareOrderId { get; set; }

        /// <summary>
        ///     订单金额，分润的金额基数，如果是商品金额，则写商品金额，如果是分润价则使用分润价
        /// </summary>
        [Display(Name = "订单金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsMain = true, LabelColor = LabelColor.Danger,
            IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, EditShow = true,
            Width = "100", ListShow = true, SortOrder = 4)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     订单金额，分润的金额基数，如果是商品金额，则写商品金额，如果是分润价则使用分润价
        /// </summary>
        [Display(Name = "总分润金额")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true, LabelColor = LabelColor.Brand,
            IsShowAdvancedSerach = true, EditShow = true,
            Width = "100", ListShow = true, SortOrder = 4)]
        public decimal TotalFenRun { get; set; }

        /// <summary>
        ///     总拨比：包括现金与非现金
        /// </summary>
        [Display(Name = "总拨比")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowBaseSerach = true,
            TableDispalyStyle = TableDispalyStyle.Code,
            IsShowAdvancedSerach = true, EditShow = true,
            Width = "100", ListShow = true, SortOrder = 4)]
        public decimal TotalRatio { get; set; }

        /// <summary>
        ///     用户收益详情
        /// </summary>
        [Display(Name = "用户收益详情")]
        public List<ShareOrderReportItem> UserItems { get; set; }

        /// <summary>
        ///     用户
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, PlaceHolder = "请输入用户名",
            DataField = "UserId", ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true,
            EditShow = true, SortOrder = 2)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [BsonIgnore]
        public string UserName { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("分润测试",
                    "/Admin/Basic/List?Service=IShareOrderReportService&Method=GetBonusPoolItems?Id=[[Id]]",
                    Icons.Coins, LinkType.TableQuickLink)
            };
            return quickLinks;
        }
    }

    [ClassProperty(Name = "分润订单详情", Icon = "fa fa-file", Description = "分润订单统计")]
    public class ShareOrderReportItem
    {
        [Display(Name = "用户ID")] public long UserId { get; set; }

        [Display(Name = "货币类型")] public Guid MoneyTypeId { get; set; }

        [Display(Name = "数量")] public decimal Amount { get; set; }
    }
}