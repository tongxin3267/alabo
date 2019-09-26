using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;
using Alabo.Mapping.Dynamic;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     用户的输出情况
    /// </summary>
    public class UserOutput : EntityDto
    {
        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     用户名（以字母开头，包括a-z,0-9和_）：[a-zA-z][a-zA-Z0-9_]{2,15}
        /// </summary>
        [Display(Name = "用户名")]
        [Field(SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [Field(SortOrder = 7)]
        public string Email { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Field(SortOrder = 4)]
        public string Mobile { get; set; }

        /// <summary>
        ///     上级用户id http://localhost:9000/user/from?id=1
        /// </summary>

        [Display(Name = "上级ID")]
        public long ParentId { get; set; } = 0;

        /// <summary>
        ///     用户状态
        /// </summary>

        [Display(Name = "用户状态")]
        public string Status { get; set; }

        /// <summary>
        ///     用户等级Id 每个类型对应一个等级
        /// </summary>
        public Guid GradeId { get; set; }

        /// <summary>
        ///     登录过期时间 为时间戳
        /// </summary>
        public double ExpireTime { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        [Display(Name = "等级")]
        [Field(SortOrder = 6)]
        public string GradeName { get; set; }

        /// <summary>
        ///     头像
        /// </summary>

        [Display(Name = "头像")]
        [Field(SortOrder = 1)]
        public string Avator { get; set; }

        /// <summary>
        ///     推荐人姓名
        /// </summary>
        [Display(Name = "推荐人")]
        [Field(SortOrder = 4)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     门店名
        /// </summary>
        [Display(Name = "所属门店")]
        public string ServiceCenterName { get; set; }

        /// <summary>
        ///     是否认证通过
        /// </summary>
        [Display(Name = "是否认证")]
        [DynamicIgnore]
        public IdentityStatus IdentityStatus { get; set; } = IdentityStatus.IsNoPost;

        /// <summary>
        ///     是否认证通过
        /// </summary>
        [Display(Name = "是否实名认证")]
        [Field(SortOrder = 4)]
        public string IdentityStatusName { get; set; }

        /// <summary>
        ///     所属区域ID
        /// </summary>
        [Display(Name = "所属区域")]
        public long RegionId { get; set; } = 0;

        /// <summary>
        ///     区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "地址")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        ///     性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        ///     等级图片
        /// </summary>
        public string GradeIcon { get; set; }

        /// <summary>
        ///     Gets or sets the open identifier.
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        ///     加密Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     店铺ID
        /// </summary>
        public dynamic Store { get; set; }

        /// <summary>
        ///     是否需要修改支付密码
        /// </summary>
        public bool IsNeedSetPayPassword { get; set; } = false;
    }
}