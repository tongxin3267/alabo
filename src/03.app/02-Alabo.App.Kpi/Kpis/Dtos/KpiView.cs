using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Kpis.Kpis.Dtos
{
    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "KPI绩效", Icon = "flaticon-route", SideBarType = SideBarType.KpiSideBar)]
    public class KpiView : BaseViewModel
    {
        /// <summary>
        ///     Id
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the user identifier.
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        [Field(ListShow = true, GroupTabId = 1, IsShowAdvancedSerach = true, IsShowBaseSerach = true, Width = "120",
            SortOrder = 2)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        [Field(ListShow = true, TableDispalyStyle = TableDispalyStyle.Code, GroupTabId = 1, IsMain = true, Width = "80",
            SortOrder = 3)]
        [Display(Name = "本次数值")]
        public decimal Value { get; set; }

        /// <summary>
        ///     Gets or sets the total value.
        /// </summary>
        /// <value>
        ///     The total value.
        /// </value>
        [Field(ListShow = true, LabelColor = LabelColor.Danger, GroupTabId = 1, Width = "100", SortOrder = 4)]
        [Display(Name = "累计绩效")]
        public decimal TotalValue { get; set; }

        /// <summary>
        ///     考核类型Id，通过
        /// </summary>
        /// <value>
        ///     The module identifier.
        /// </value>

        public Guid ModuleId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the module.
        /// </summary>
        /// <value>
        ///     The name of the module.
        /// </value>
        [Field(ListShow = true, ControlsType = ControlsType.DateTimeRang, GroupTabId = 1, Width = "150", SortOrder = 1)]
        [Display(Name = "考核类型")]
        public string Module { get; set; }

        /// <summary>
        ///     Gets or sets the create time.
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Field(ListShow = true, ControlsType = ControlsType.DateTimeRang, GroupTabId = 1, Width = "180",
            SortOrder = 10)]
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}