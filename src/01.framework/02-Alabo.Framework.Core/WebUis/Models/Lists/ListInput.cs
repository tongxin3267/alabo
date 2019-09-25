using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Themes.DiyModels.Base;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Themes.DiyModels.Lists {

    /// <summary>
    ///     zk-list 分页数据
    /// </summary>
    [ClassProperty(Name = "链接")]
    public class ListInput : BaseComponent {

        /// <summary>
        ///     数据ID
        /// </summary>
        [Display(Name = "数据序号")]
        public string DataId {
            get; set;
        }

        /// <summary>
        ///     客户端当前登录用户的Id
        /// </summary>
        [Display(Name = "客户端当前登录用户的Id")]
        public long LoginUserId {
            get; set;
        }

        /// <summary>
        ///     当前页
        /// </summary>
        [Display(Name = "分页")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long PageIndex { get; set; } = 1;

        /// <summary>
        ///     分页大小
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        [Display(Name = "分页大小")]
        public long PageSize { get; set; } = 15;

        /// <summary>
        /// 终端类型
        /// </summary>
        public ClientType ClientType {
            get; set;
        }
    }
}