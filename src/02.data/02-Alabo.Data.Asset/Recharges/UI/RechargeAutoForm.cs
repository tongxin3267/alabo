using System;
using Alabo.App.Asset.Recharges.Dtos;
using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Recharges.UI
{
    [ClassProperty(Name = "充值", Description = "充值AutoForm")]
    public class RechargeAutoForm : UIBase, IAutoForm
    {
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            return ToAutoForm(new RechargeOnlineAddInput());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            throw new NotImplementedException();
        }
    }
}