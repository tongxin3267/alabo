using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Tenants;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.Domain.Entities {

    /// <summary>
    ///     Invoice 类存放收银台发票记录
    ///     收银台支付信息
    ///     账单,收款请求
    /// </summary>
    [ClassProperty(Name = "收银台", Icon = "fa fa-puzzle-piece", Description = "收银台",
        SideBarType = SideBarType.FinancePaySideBar)]
    public class Pay : AggregateDefaultUserRoot<Pay> {

        /// <summary>
        ///     结算类型
        ///     支付订单类型
        /// </summary>
        [Display(Name = "订单类型")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true, Width = "150",
            DataSource = "Alabo.Core.Enums.Enum.CheckoutType", ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, SortOrder = 5)]
        public CheckoutType Type { get; set; } = 0;

        /// <summary>
        ///     支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true, Width = "150",
            DataSource = "Alabo.App.Finance.Domain.Enums.PayType", ListShow = true, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, SortOrder = 6)]
        public PayType PayType { get; set; } = 0;

        /// <summary>
        ///     获取或设置实体 Id
        ///     如果商品是商品订单的时候，对应ZkShop_Order id
        ///     用Json格式保存
        /// </summary>
        [Display(Name = "获取或设置实体 Id")]
        public string EntityId {
            get; set;
        }

        /// <summary>
        ///     需要支付的金额
        /// </summary>
        [Display(Name = "订单金额")]
        [Field(ControlsType = ControlsType.NumberRang, EditShow = true, Width = "180", IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, ListShow = true, IsMain = true, SortOrder = 3)]
        public decimal Amount {
            get; set;
        }

        /// <summary>
        ///     使用账户支付的部分
        /// </summary>
        [Display(Name = "使用账户支付的部分")]
        public string AccountPay {
            get; set;
        }

        /// <summary>
        ///     获取或设置是否回调完成
        /// </summary>
        [Display(Name = "是否回调完成")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true,
            DataSource = "Alabo.Core.Enums.Enum.PayStatus",
            IsShowBaseSerach = true, IsShowAdvancedSerach = true, Width = "180", ListShow = true, SortOrder = 7)]
        public PayStatus Status { get; set; } = PayStatus.WaiPay;

        /// <summary>
        ///     支付流水号
        ///     回调反馈回来的支付订单号
        ///     第三方支付接口返回来的数据
        /// </summary>
        [Display(Name = "支付流水号")]
        [Field(ControlsType = ControlsType.TextBox, TableDispalyStyle = TableDispalyStyle.Code, IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, EditShow = true, Width = "180", ListShow = true, SortOrder = 4)]
        public string ResponseSerial { get; set; } = string.Empty;

        /// <summary>
        ///     第三方支付返回来的数据信息
        ///     以JSon的格式保存
        /// </summary>
        [Display(Name = "第三方支付返回来的数据信息")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     支付订单扩展数据
        /// </summary>
        [Display(Name = "支付订单扩展数据")]
        public string Extensions { get; set; } = string.Empty;

        /// <summary>
        ///     反应时间
        ///     第三方支付回调时间
        /// </summary>
        [Display(Name = "支付回调时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "130", ListShow = true, SortOrder = 2000)]
        public DateTime ResponseTime { get; set; } = DateTime.MinValue;

        /// <summary>
        ///     扩展实体
        /// </summary>
        [Display(Name = "扩展实体")]
        public PayExtension PayExtension { get; set; } = new PayExtension();

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        [NotMapped]
        [Display(Name = "用户")]
        public Users.Entities.User User {
            get; set;
        }

        /// <summary>
        ///     使用账户支付的部分
        ///     键值对
        /// </summary>
        [Display(Name = "使用账户支付的部分")]
        public IList<KeyValuePair<Guid, decimal>> AccountPayPair {
            get; set;
        } =
            new List<KeyValuePair<Guid, decimal>>();

        /// <summary>
        ///     Views the links.
        /// </summary>
        [Display(Name = "查看链接")]
        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("编辑", "/Admin/Bill/Pay?id=[[Id]]", Icons.Edit, LinkType.ColumnLink)
            };
            return quickLinks;
        }
    }

    public class InvoiceTableMap : MsSqlAggregateRootMap<Pay> {

        protected override void MapTable(EntityTypeBuilder<Pay> builder) {
            builder.ToTable("Finance_Pay");
        }

        protected override void MapProperties(EntityTypeBuilder<Pay> builder) {
            //应用程序编号
            builder.Ignore(e => e.PayExtension);
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.User);
            builder.Ignore(e => e.AccountPayPair);
            builder.Ignore(e => e.Version);
            if (TenantContext.IsTenant) {
                // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
            }
        }
    }
}