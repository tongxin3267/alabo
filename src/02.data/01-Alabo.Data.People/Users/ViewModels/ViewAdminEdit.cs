using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Users.Entities;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.User.ViewModels {

    /// <summary>
    ///     后台用户编辑视图
    /// </summary>
    public class ViewAdminEdit : BaseViewModel {

        /// <summary>
        /// 登录用户ID
        /// </summary>
        public long LoginUserId { get; set; }

        /// <summary>
        /// 要Edit的用户ID
        /// </summary>
        public long EditUserId { get; set; }

        /// <summary>
        /// 所属门店
        /// </summary>
        [Display(Name = "所属门店")]
        public string ServiceCenterName { get; set; }

        /// <summary>
        ///     Gets or sets the 会员.
        /// </summary>
        public Users.Entities.User User { get; set; }

        /// <summary>
        ///     Gets or sets the 会员 detail.
        /// </summary>
        public UserDetail UserDetail { get; set; }

        /// <summary>
        ///     门店用户名，不是数据库字段
        /// </summary>
        [Display(Name = "所属门店用户")]
        public string ServiceCenterUserName { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        [Display(Name = "状态")]
        public Status Status { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     修改密码的类型 1表示修改登录密码，2表示修改支付密码,3表示修改详情，0表示修改基本资料
        /// </summary>
        public int Type { get; set; } = 0;

        /// <summary>
        ///     将修改的密码发送短信通知
        /// </summary>
        public bool SendPassword { get; set; } = true;

        /// <summary>
        ///     Gets or sets the sex.
        /// </summary>
        [Display(Name = "性别")]
        public Sex Sex { get; set; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        public Users.Entities.User Parent { get; set; }

        /// <summary>
        ///     所属区域
        /// </summary>
        [Display(Name = "所属区域")]
        public long RegionId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "详细地址")]
        public string Address { get; set; }

        /// <summary>
        ///     区域名称
        /// </summary>
        public string RegionName { get; set; }

        public UserGradeConfig UserGradeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the avator.
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        ///     实名认证状态
        /// </summary>
        public IdentityStatus IdentityStatus { get; set; } = IdentityStatus.IsNoPost;

        /// <summary>
        /// 会员等级列表
        /// </summary>
        public List<UserGradeConfig> GradeList { get; set; }

        public object StatusList { get; set; }
    }
}