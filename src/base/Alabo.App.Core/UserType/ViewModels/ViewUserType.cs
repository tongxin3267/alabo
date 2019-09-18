using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.ViewModels {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class ViewUserType : BaseViewModel {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        public long Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        public string Name { get; set; }

        /// <summary>
        ///     用户类型ID
        /// </summary>
        /// <value>
        ///     The user type identifier.
        /// </value>
        [Display(Name = "用户类型")]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [Display(Name = "用户名称")]
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the entity identifier.
        /// </summary>
        /// <value>
        ///     The entity identifier.
        /// </value>
        public long EntityId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        ///     The name of the entity.
        /// </value>
        public string EntityName { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        /// <value>
        ///     The parent user identifier.
        /// </value>
        [Display(Name = "推荐人")]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the parent user.
        /// </summary>
        /// <value>
        ///     The name of the parent user.
        /// </value>
        public string ParentUserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        [Display(Name = "等级Id")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     Gets or sets the base grade configuration.
        /// </summary>
        /// <value>
        ///     The base grade configuration.
        /// </value>
        public BaseGradeConfig BaseGradeConfig { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        /// <value>
        ///     The name of the grade.
        /// </value>
        [Display(Name = "等级名称")]
        public string GradeName { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public UserTypeStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets the create time.
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        public DateTime CreateTime { get; set; }
    }
}