using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Circles.Domain.Configs
{
    /// <summary>
    ///     商圈配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "商圈配置", Icon = "fa fa-user-times", Description = "商圈配置", PageType = ViewPageType.Edit, SortOrder = 12)]
    public class CircleConfig : IAutoConfig
    {
        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "商圈服务条款")]
        [HelpBlock("请输入商圈服务条款，默认值为商圈服务条款")]
        public string CircleServiceAgreement { get; set; } = "商圈服务条款";

        public void SetDefault()
        {
        }
    }
}