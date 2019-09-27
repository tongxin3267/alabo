using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Mapping;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Extensions;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.ViewModels.UI
{
    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "店铺管理", Icon = "la la-users", Description = "店铺管理",
        SideBarType = SideBarType.SupplierSideBar)]
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