using Alabo.App.Core.Finance.Domain.Dtos.Transfer;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.UI.AutoForm {

    [ClassProperty(Name = "转帐AutoForm", Description = "转帐")]
    public class TransferAutoForm : UIBase, IAutoForm {

        public Alabo.UI.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel) {
            return ToAutoForm(new TransferAddInput());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var temp = (TransferAddInput)model;
            var result = Resolve<ITransferService>().Add(temp);
            return result;
        }
    }
}