using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Cloud.People.Identities.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Users.Enum;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.People.Identities.UI
{
    /// <summary>
    ///     实名认证
    /// </summary>
    [Display(Name = "实名认证")]
    [ClassProperty(Name = "实名认证", Description = "实名认证")]
    public class IdentityAutoForm : UIBase, IAutoForm
    {
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var model = Ioc.Resolve<IIdentityService>().GetSingle(u => u.UserId == id.ConvertToLong(0));
            var result = new AutoForm();
            if (model != null)
            {
                if (model.Status == IdentityStatus.Succeed)
                    result.FromMessage = new FromMessage(FromMessageType.Success, "恭喜您,实名认证已成功");
                if (model.Status == IdentityStatus.Failed) result = ToAutoForm(new Identity());
            }
            else
            {
                result = ToAutoForm(new Identity());
            }

            result.AlertText = "【个人认证】对用户资料真实性进行的一种验证审核。有助于建立完善可靠的互联网信用平台。";
            result.ButtomHelpText = new List<string>
            {
                "真实姓名：务必填写真实姓名，以身份证姓名为准",
                "身份证号码：务必填写身份证号码，以身份证号码为准"
            };

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var identity = (Identity) model;
            var result = Resolve<IIdentityService>().Identity(identity);
            return result;
        }
    }
}