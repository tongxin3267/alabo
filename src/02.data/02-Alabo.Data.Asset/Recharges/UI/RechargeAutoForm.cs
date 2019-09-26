using System;
using Alabo.App.Core.Finance.Domain.Dtos.Recharge;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "充值", Description = "充值AutoForm")]
    public class RechargeAutoForm : UIBase, IAutoForm {

        public Alabo.Core.WebUis.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            return ToAutoForm(new RechargeOnlineAddInput());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            throw new NotImplementedException();
        }
    }
}