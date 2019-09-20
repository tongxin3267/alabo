using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Dtos {

    public class MpLoginInput {
        public string code { get; set; }

        /// <summary>用户昵称</summary>
        public string nickname { get; set; }

        /// <summary>用户的性别，值为1时是男性，值为2时是女性，值为0时是未知</summary>
        public int gender { get; set; }

        /// <summary>用户个人资料填写的省份</summary>
        public string province { get; set; }

        /// <summary>普通用户个人资料填写的城市</summary>
        public string city { get; set; }

        /// <summary>国家，如中国为CN</summary>
        public string country { get; set; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string avatarUrl { get; set; }
    }

    /// <summary>
    ///     登录模块
    /// </summary>
    public class MpBindInput {

        /// <summary>
        ///     Gets or sets the open identifier.
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(128, MinimumLength = 11, ErrorMessage = ErrorMessage.StringLength)]
        public string Mobile { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [MinLength(6, ErrorMessage = "验证码格式不正确")]
        [MaxLength(6, ErrorMessage = "验证码格式不正确")]
        [Field(ControlsType = ControlsType.PhoneVerifiy, GroupTabId = 2, Mark = "Mobile", EditShow = true)]
        public string MobileVerifiyCode { get; set; }

        [Display(Name = "当前账号id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CurUserId { get; set; }
    }
}