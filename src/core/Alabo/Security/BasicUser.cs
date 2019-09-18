using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Security.Enums;

namespace Alabo.Security
{
    /// <summary>
    ///     基础用户
    /// </summary>
    public class BasicUser
    {
        /// <summary>
        ///     用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     上级用户id
        /// </summary>
        [Display(Name = "上级用户id")]
        public long ParentId { get; set; } = 0;

        /// <summary>
        ///     用户状态
        /// </summary>
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     用户等级Id 每个类型对应一个等级
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     当前租户
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        ///     登录方式
        /// </summary>
        public LoginAuthorizeType LoginAuthorizeType { get; set; } = LoginAuthorizeType.Login;
    }
}