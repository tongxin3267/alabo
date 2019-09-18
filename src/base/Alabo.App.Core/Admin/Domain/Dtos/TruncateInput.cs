using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Admin.Domain.Dtos {

    /// <summary>
    /// 清空数据
    /// </summary>
    [ClassProperty(Name = "清空数据", Icon = IconFlaticon.exclamation, SideBarType = SideBarType.ControlSideBar)]
    public class TruncateInput : BaseViewModel {

        /// <summary>
        /// 激活Key
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "激活Key")]
        [HelpBlock("请联系您的服务商获取，格式示列: b0000000-2600-4000-bc3f-e00000000000<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1)]
        public string ProjectId { get; set; }

        /// <summary>
        /// 激活秘钥
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "激活秘钥")]
        [HelpBlock(" 请联系您的服务商获取，格式示列: NZBZNQIYDT********WVA4HSDNUW6FPMFO<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 2)]
        public string Key { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "管理员支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, SortOrder = 300)]
        [HelpBlock("输入当前管理员密码，必须是超级管理员Admin才可以操作该功能<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string PayPassword { get; set; }

        /// <summary>
        /// 平台预留手机
        /// </summary>
        [Display(Name = "平台预留手机")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 4)]
        [HelpBlock("请联系您的服务商获取<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string Mobile { get; set; }

        /// <summary>
        /// 用户表的名称
        /// </summary>
        [Display(Name = "用户表的名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 5)]
        [HelpBlock("请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string UserTable { get; set; }

        /// <summary>
        /// 用户表的名称
        /// </summary>
        [Display(Name = "调度中心表名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 6)]
        [HelpBlock("调度中心表名,请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string ScheduleTable { get; set; }

        ///// <summary>
        ///// 平台预留手机
        ///// </summary>
        //[Display(Name = "Sql数据库名")]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //[Field(ControlsType = ControlsType.TextBox, SortOrder = 7)]
        //[HelpBlock("<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>请正确填写确保您是专业的运维人员")]
        //public string SqlTableName { get; set; }

        /// <summary>
        /// Mongodb数据库名称
        /// </summary>
        [Display(Name = "Mongodb数据库名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 8)]
        [HelpBlock("请正确填写确保您是专业的运维人员<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string MongoTableName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 8)]
        [HelpBlock("请填写当前项目正确的公司名称<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string CompanyName { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        // [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        // [MinLength(6, ErrorMessage = ErrorMessage.MinStringLength)]
        public string MobileVerifiyCode { get; set; }
    }
}