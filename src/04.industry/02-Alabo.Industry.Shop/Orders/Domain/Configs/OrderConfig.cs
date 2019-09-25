using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Common;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Order.Domain.CallBacks {

    /// <summary>
    ///     订单配置
    /// </summary>
    [ClassProperty(Name = "订单配置", Icon = "fa fa-puzzle-piece", SortOrder = 1,
        Description = "订单配置",
        SideBarType = SideBarType.OrderSideBar)]
    public class OrderConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     订单自动关闭时间，默认1天，订单关闭后，库存恢复
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 6, ListShow = true)]
        [Required]
        [Display(Name = "订单自动关闭时间")]
        [Main]
        [HelpBlock("订单自动关闭时间，默认1天，订单关闭后，库存恢复")]
        public long OrderClosedDay { get; set; } = 1;

        /// <summary>
        /// 订单自动关闭时间 默认两小时
        /// </summary>
        public long OrderClosedHour { get; set; } = 2;

        /// <summary>
        /// 自动确认收货 默认15天
        /// </summary>
        public long OrderTakeDay { get; set; } = 15;

        [Display(Name = "购买是是否需要实名认证")]
        [Field(ControlsType = ControlsType.Switch, SortOrder = 6, ListShow = true)]
        public bool IsIdentity { get; set; } = false;


        /// <summary>
        /// 现金是否可以抵其他虚拟资产下单的时候
        /// 比如：积分不足时，是否可以用现金购买
        /// </summary>
        public bool CashCanToOtherAssets { get; set; } = false;

        public void SetDefault() {
        }
    }
}