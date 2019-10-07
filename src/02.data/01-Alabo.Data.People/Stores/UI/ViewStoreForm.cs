using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Data.People.Stores.UI
{
    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "店铺管理", Icon = "la la-users", Description = "店铺管理")]
    public class ViewStoreForm : Store, IAutoForm
    {
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var viewStore = Resolve<IStoreService>().GetViewById(id);
            var model = AutoMapping.SetValue<ViewStoreForm>(viewStore);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var store = AutoMapping.SetValue<Store>(model);
            var result = Resolve<IStoreService>().AddOrUpdate(store, store.Id.IsObjectIdNullOrEmpty());
            if (!result)
            {
                return ServiceResult.FailedWithMessage("店铺更新失败");
            }
            return ServiceResult.Success;
        }
    }
}