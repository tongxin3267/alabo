using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Alabo.Web.Validations;

namespace Alabo.App.Core.Tasks.Domain.ViewModels {

    /// <summary>
    ///     分润订单
    /// </summary>
    [ClassProperty(Name = "分润测试", Icon = "fa fa-file", Description = "分润测试",
        SideBarType = SideBarType.ShareOrderTestSideBar)]
    public class TestShareOrderView : UIBase, IAutoForm {

        /// <summary>
        /// 要更改的用户名
        /// </summary>
        [Display(Name = "要更改的用户名")]
        [Required(ErrorMessage = "请输入要更改的用户名")]
        [HelpBlock("请输入要更改的用户名")]
        [Field(EditShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox, ValidType = ValidType.UserName)]
        public string UserName { get; set; }

        /// <summary>
        ///     订单金额，分润的金额基数，如果是商品金额，则写商品金额，如果是分润价则使用分润价
        /// </summary>
        [Display(Name = "订单金额")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            EditShow = true,
            Width = "100", ListShow = true, SortOrder = 2)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     操作记录备注信息
        /// </summary>
        [Display(Name = "操作备注")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 3, Width = "110", ListShow = true)]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// 支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(EditShow = true, SortOrder = 4, ControlsType = ControlsType.Password)]
        [HelpBlock("输入当前用户的支付密码")]
        [DataType(DataType.Password)]
        public string PayPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new TestShareOrderView();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = model.MapTo<TestShareOrderView>();
            var user = Resolve<IUserService>().GetSingle(view.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            if (view.Amount <= 0) {
                return ServiceResult.FailedWithMessage("分润金额不能小于0");
            }

            ShareOrder shareOrder = new ShareOrder {
                UserId = user.Id,
                EntityId = user.Id,
                Amount = view.Amount,
                TriggerType = TriggerType.Other,
            };
            if (!Resolve<IShareOrderService>().Add(shareOrder)) {
                return ServiceResult.FailedWithMessage("添加失败");
            }
            return ServiceResult.Success;
        }
    }
}