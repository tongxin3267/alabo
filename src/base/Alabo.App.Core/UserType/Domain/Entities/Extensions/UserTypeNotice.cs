using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Extensions;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Entities.Extensions {

    /// <summary>
    ///     店铺公告
    /// </summary>
    public class UserTypeNotice : EntityExtension {
        public Guid Id { get; set; }

        /// <summary>
        ///     公告标题
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "标题不能超过100个字")]
        [Display(Name = "公告标题")]
        public string Title { get; set; }

        /// <summary>
        ///     公告内容
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "公告内容")]
        public string Content { get; set; }

        /// <summary>
        ///     所属店铺
        /// </summary>
        public long UserTypeId { get; set; }

        /// <summary>
        ///     所属会员
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     排序,越小排在越前面
        /// </summary>
        [Display(Name = "排序", Order = 1000)]
        [Range(0, 99999, ErrorMessage = "请输入0-99999之间的数字")]
        [HelpBlock("排序,越小排在越前面，请输入0-99999之间的数字")]
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     通用状态 状态：0正常,1冻结,2删除
        ///     实体的软删除通过此字段来实现
        ///     软删除：指的是将实体标记为删除状态，不是真正的删除，可以通过回收站找回来
        /// </summary>
        [Display(Name = "状态")]
        public Status Status { get; set; } = Status.Normal;
    }
}