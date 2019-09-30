using Alabo.Cloud.Support.Domain.Dtos;
using Alabo.Cloud.Support.Domain.Entities;
using Alabo.Cloud.Support.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.Support.UI.AutoForm
{
    [ClassProperty(Name = "意见反馈AutoForm", Description = "意见反馈")]
    public class WorkOrderAutoForm : UIBase, IAutoForm
    {
        public Alabo.UI.Design.AutoForms.AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            return ToAutoForm(new WorkOrderInput());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var temp = (WorkOrderInput)model;
            var item = AutoMapping.SetValue<WorkOrder>(temp);
            var result = Resolve<IWorkOrderService>().AddWorkOrder(item);
            return result;
        }
    }
}