using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Core.Admin;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Cloud.Core.Truncate;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace _01_Alabo.Cloud.Core.RepairData {
    /// <summary>
    /// 修复数据
    /// </summary>

    [ClassProperty(Name = "清空数据", Icon = IconFlaticon.exclamation)]
    public class RepairDataForm : UIBase, IAutoForm {

        [Display(Name = "管理员支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.Password, SortOrder = 300)]
        [HelpBlock("输入当前管理员密码，必须是超级管理员Admin才可以操作该功能<br/><code>该操作风险极大，请确保已备份Mongodb和Sql数据</code><br/><code>若您非运维人员请勿操作</code>")]
        public string PayPassword { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var view = new RepairDataForm();
            return ToAutoForm(view);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var truncateInput = model.MapTo<RepairDataForm>();
            // 其他项目的默认数据
            var types = Resolve<ITypeService>().GetAllTypeByInterface(typeof(ICheckData));
            foreach (var item in types) {
                var config = Activator.CreateInstance(item);
                if (config is ICheckData set) {
                    set.Execute();
                    set.ExcuteAsync().GetAwaiter();
                }
            }
            return ServiceResult.Success;
        }
    }
}