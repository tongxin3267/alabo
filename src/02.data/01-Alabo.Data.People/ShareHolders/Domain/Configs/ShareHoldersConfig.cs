using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.ShareHolders.Domain.Configs
{
    /// <summary>
    ///     股东配置
    /// </summary>
    /// <summary>
    ///     股东等级
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "股东配置", Icon = "fa fa-user-times",
        Description = "股东配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.ShareHoldersSideBar)]
    public class ShareHoldersConfig : IAutoConfig
    {
        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "股东服务条款")]
        [HelpBlock("请输入股东服务条款，默认值为股东服务条款")]
        public string ShareHoldersServiceAgreement { get; set; } = "股东服务条款";

        public void SetDefault()
        {
        }
    }
}