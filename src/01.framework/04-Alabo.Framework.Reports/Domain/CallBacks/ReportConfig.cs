using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Framework.Reports.Domain.CallBacks
{
    /// <summary>
    ///     统计设置
    /// </summary>
    [ClassProperty(Name = "统计设置", Icon = "fa fa-calendar", SortOrder = 500,
        Description = "设置以及查看系统的统计设置", SideBarType = SideBarType.ControlSideBar)]
    [NotMapped]
    public class ReportConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     统计周期
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [Display(Name = "统计周期")]
        [HelpBlock("单位为分钟，统计的周期，默认为10分钟，统计一次所有的数据,统计一次所消耗的时间和服务器资源比较大，建议不要设置过小")]
        public long Cycle { get; set; } = 10;

        /// <summary>
        ///     默认数据
        /// </summary>
        public void SetDefault()
        {
        }
    }
}