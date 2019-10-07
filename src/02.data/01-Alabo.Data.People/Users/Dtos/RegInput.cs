using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     注册模块
    /// </summary>
    [ClassProperty(Name = "会员注册", GroupName = "推荐人信息,基本信息")]
    public class RegInput : EntityDto
    {
        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        ///     确认密码
        /// </summary>
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        public string PayPassword { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string Mobile { get; set; }

        /// <summary>
        ///     Gets or sets the open identifier.
        /// </summary>
        [Display(Name = "OpenId")]
        public string OpenId { get; set; }

        /// <summary>
        ///     手机验证码
        /// </summary>
        [Display(Name = "手机验证码")]
        public string VerifiyCode { get; set; }

        /// <summary>
        ///     推荐人用户名
        /// </summary>
        [Display(Name = "推荐人用户名")]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     服务中心
        ///     门店用户
        /// </summary>
        [Display(Name = "服务中心")]
        public string ServiceCenter { get; set; }

        /// <summary>
        ///     Gets or sets the parent identifier.
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否同意注册协议
        /// </summary>
        public bool Agree { get; set; }
    }
}