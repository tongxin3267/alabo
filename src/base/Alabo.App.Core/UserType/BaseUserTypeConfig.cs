using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType {

    /// <summary>
    ///     用户类型配置基类
    /// </summary>
    public class BaseUserTypeConfig : AutoConfigBase {
        //前台是否允许申请
        //申请时是否需要等级
        //是否需要所在区域
        //是否需要服务条款
        //申请是跳转到收银台  （如果付款成功就不需要审核）

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "服务条款")]
        [HelpBlock("请输入服务条款，默认值为服务条款")]
        public string ServiceAgreement { get; set; } = "服务条款";
    }
}