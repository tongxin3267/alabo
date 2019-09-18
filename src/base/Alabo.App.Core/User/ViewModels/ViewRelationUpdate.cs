using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.UI;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Validations;

namespace Alabo.App.Core.User.ViewModels {

    /// <summary>
    /// 修改推荐关系
    /// </summary>
    [ClassProperty(Name = "修改推荐关系", SideBarType = SideBarType.OrganizationalChartSideBar, ListApi = "Api/UserMap/GetTransferRelationship")]
    public class ViewRelationUpdate : UIBase, IAutoForm {

        /// <summary>
        /// 当前登录id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        /// 要更改的用户名
        /// </summary>
        [Display(Name = "要更改的用户名")]
        [Required(ErrorMessage = "请输入要更改的用户名")]
        [HelpBlock("修改推荐人，具有一定的风险，修改后需确保组织架构图正确，否则会影响分润的正确性")]
        [Field(EditShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// 上级用户名(推荐人)
        /// </summary>
        [Display(Name = "上级用户名(推荐人)")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("新推荐人用户名，用户状态需正常")]
        [Field(EditShow = true, SortOrder = 2, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string ParentUserName { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(EditShow = true, SortOrder = 3, ControlsType = ControlsType.Password)]
        [HelpBlock("输入当前用户的支付密码")]
        [DataType(DataType.Password)]
        public string PayPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new ViewRelationUpdate();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = model.MapTo<ViewRelationUpdate>();
            if (autoModel != null) {
                view.UserId = autoModel.BasicUser.Id;
            }

            var result = Resolve<IUserMapService>().UpdateParentUser(view);
            return result;
        }
    }

    /// <summary>
    /// 转移团队关系
    /// </summary>

    [ClassProperty(Name = "转移团队关系", Description = "将转移用户的所有团队，转移到接受用户的旗下。", SideBarType = SideBarType.OrganizationalChartSideBar, Icon = IconFontawesome.trademark, ListApi = "Api/UserMap/GetUpdateParentUserView")]
    public class ViewTransferRelationship : UIBase, IAutoForm {

        /// <summary>
        /// 当前登录id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public long UserId { get; set; }

        /// <summary>
        /// 需转移的用户名
        /// </summary>
        [Display(Name = "需转移用户名")]
        [Required(ErrorMessage = "需转移的用户名")]
        [HelpBlock("需转移的用户名，具有一定的风险，修改后需确保组织架构图正确，否则会影响分润的正确性<br/><code>确定后会将该用户所有的团队人转移到接受人旗下</code>")]
        [Field(EditShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string UserName { get; set; }

        /// <summary>
        /// 上级用户名(推荐人)
        /// </summary>
        [Display(Name = "接受用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("接受用户名，用户状态需正常<br/><code>确定后该用户,将接受转移用户名的所有团队</code>")]
        [Field(EditShow = true, SortOrder = 2, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string ParentUserName { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(EditShow = true, SortOrder = 3, ControlsType = ControlsType.Password)]
        [HelpBlock("输入当前用户的支付密码")]
        [DataType(DataType.Password)]
        public string PayPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new ViewTransferRelationship();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = model.MapTo<ViewTransferRelationship>();
            if (autoModel != null) {
                view.UserId = autoModel.BasicUser.Id;
            }
            var result = Resolve<IUserMapService>().TransferRelationship(view);
            return result;
        }
    }
}