using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Tasks.Domain.CallBacks {

    [ClassProperty(Name = "奖金池", Icon = IconFlaticon.menu, SortOrder = 1,
        SideBarType = SideBarType.BonusPoolBar)]
    /// <summary>
    ///     奖金池配置
    /// </summary>
    public class BonusPoolConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     奖金池基数比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        [Display(Name = "奖金池基数比例")]
        [HelpBlock("奖金池基数比例，单位请填写0-1之间的数值，0.5表示50%，系统30分钟统计一次，可以分润报表统计中手动触发，完成统计")]
        public decimal BaseRadio { get; set; } = 0.5m;

        /// <summary>
        ///     奖金池统计方式
        /// </summary>
        [Display(Name = "奖金池统计方式")]
        [Field(ControlsType = ControlsType.RadioButton, ListShow = true, EditShow = true,
            SortOrder = 2, Width = "110", DataSource = "Alabo.App.Core.Tasks.Domain.Enums.BonusPoolType")]
        [HelpBlock("奖金池统计方式以奖金池基数比例为依据，包括按分润订单金额保留和按分润订单总分润金额保留两种方式，修改比例时请手动触发统计一次")]
        public BonusPoolType BonusPoolType { get; set; } = BonusPoolType.ByShareOrderAmount;

        /// <summary>
        ///     上一次统计的ShareOrderId
        /// </summary>
        [Display(Name = "上一次统计的共享订单ID")]
        public long LastShareOrderId { get; set; }

        /// <summary>
        ///     奖金池总金额
        /// </summary>
        [Display(Name = "奖金池总金额")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     最大拨比
        /// </summary>
        [Display(Name = "最大拨比")]
        public decimal MaxRadio { get; set; }

        /// <summary>
        ///     最小拨比
        /// </summary>
        [Display(Name = "最小拨比")]
        public decimal MinRadio { get; set; } = 1;

        /// <summary>
        ///     上次统计时间
        /// </summary>
        [Display(Name = "上次统计时间")]
        public DateTime LastReportTime { get; set; }

        /// <summary>
        ///     最高分润金额
        /// </summary>
        [Display(Name = "最高分润金额")]
        public decimal MaxShareAmount { get; set; }

        /// <summary>
        ///     订单最大金额
        /// </summary>
        [Display(Name = "订单最大金额")]
        public decimal MaxAmount { get; set; }

        public void SetDefault() {
        }
    }
}