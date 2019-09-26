using System.ComponentModel.DataAnnotations;
using Alabo.App.Kpis.Kpis.Domain.Enum;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Kpis.GradeKpis.Dtos
{
    /// <summary>
    ///     修改会员等级
    /// </summary>
    [ClassProperty(Name = "修改会员等级", SideBarType = SideBarType.KpiSideBar)]
    public class GradeChangeView : BaseViewModel
    {
        /// <summary>
        ///     Id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public string Id { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "考核结果")]
        [Field(ControlsType = ControlsType.Label, IsTabSearch = true, ListShow = true, EditShow = true,
            SortOrder = 1, Width = "110", DataSource = "KpiResult")]
        public string UserName { get; set; }

        /// <summary>
        ///     考核结果
        /// </summary>
        [Display(Name = "考核结果")]
        [Field(ControlsType = ControlsType.Label, IsTabSearch = true, ListShow = true, EditShow = true,
            SortOrder = 2, Width = "110", DataSource = "KpiResult")]
        public KpiResult KpiResult { get; set; }

        /// <summary>
        ///     当前用户等级Id
        /// </summary>
        [Display(Name = "当前会员等级")]
        [Field(ControlsType = ControlsType.Label, SortOrder = 3)]
        public string GradeName { get; set; }

        /// <summary>
        ///     修改后会员等级
        /// </summary>
        [Display(Name = "修改后会员等级")]
        [Field(ControlsType = ControlsType.Label, SortOrder = 4)]
        public string ChangeGradeName { get; set; }
    }
}