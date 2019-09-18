using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.App.Core.UserType.Domain.Entities.Extensions {

    /// <summary>
    ///     用户类型附加数据
    /// </summary>
    public class UserTypeExtensions : EntityExtension {

        /// <summary>
        ///     角色Id,与岗位角色Id对应
        ///     与PostRoleConfig 相对应
        /// </summary>
        public Guid RoldId { get; set; }

        [Display(Name = "简介")] public string Intro { get; set; }

        public string Address { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     备注，此备注一般表示管理员备注，前台会员不可以修改
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     店铺公告
        /// </summary>
        public List<UserTypeNotice> Notices { get; set; }
    }
}