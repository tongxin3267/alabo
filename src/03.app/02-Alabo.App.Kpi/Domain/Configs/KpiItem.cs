using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Share.Kpi.Domain.CallBack {

    /// <summary>
    ///     Kpi考核项
    /// </summary>
    [ClassProperty(Name = "Kpi考核基准配置")]
    public class KpiItem : BaseViewModel {

        /// <summary>
        ///     唯一标识(不能重复)
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true,
            SortOrder = 2)]
        [Display(Name = "唯一标识(大小字母A-Z)")]
        public string Key { get; set; }

        /// <summary>
        ///     Kpi配置
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, DataSource = "KpiAutoConfig",
            SortOrder = 3)]
        public Guid KpiConfigId { get; set; }

        /// <summary>
        ///     预算符
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, DataSource = "OperatorCompare",
            SortOrder = 4)]
        [Display(Name = "预算符")]
        public OperatorCompare OperatorCompare { get; set; } = OperatorCompare.GreaterEqual;

        /// <summary>
        ///     考核基准金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 5)]
        [Display(Name = "考核基准金额")]
        public decimal Amount { get; set; } = 0.0m;
    }
}