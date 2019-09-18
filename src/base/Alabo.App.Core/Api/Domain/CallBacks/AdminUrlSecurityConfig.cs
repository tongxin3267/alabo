using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Api.Domain.CallBacks {

    [ClassProperty(Name = "后台访问安全", Icon = "fa fa-warning", SortOrder = 1, Description = "设置独立的访问后台地址，让您的系统访问更安全",
        SideBarType = SideBarType.ControlSideBar)]
    public class AdminUrlSecurityConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     访问签名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, Row = 12, SortOrder = 1)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock(
            "为您系统的安全，请定期修改新的签名，签名请妥善保管，请勿泄露！<br/>后台访问格式：<code>http://www.xxx.com/Admin/Login?Key=Sign</code> <br/>其中【http://www.xxx.com】为您的访问网址,【Sign】为您的签名<br/>长度<b>5-100</b>个字符,不能与系统预览的字符串相同<br/>")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = ErrorMessage.StringLength)]
        public string Sign { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, GroupTabId = 1, SortOrder = 300)]
        [HelpBlock("请输入您的支付密码")]
        public string PayPassword { get; set; }

        public void SetDefault() {
        }
    }
}