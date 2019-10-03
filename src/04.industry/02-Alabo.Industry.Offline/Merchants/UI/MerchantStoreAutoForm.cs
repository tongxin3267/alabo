using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using Alabo.Industry.Offline.Merchants.Domain.Services;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.UI
{
    /// <summary>
    ///     店铺设置
    /// </summary>
    [Display(Name = "店铺设置")]
    [ClassProperty(Name = "店铺设置", Description = "店铺设置")]
    public class MerchantStoreAutoForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     StoreId
        ///     MapToString for complex type ObjectId
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string StoreId { get; set; }

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public long UserId { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string Name { get; set; }

        /// <summary>
        ///     门店logo
        /// </summary>
        [Display(Name = "门店logo")]
        public string Logo { get; set; }

        /// <summary>
        ///     门店描述
        /// </summary>
        [Display(Name = "门店描述")]
        public string Description { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var autoForm = new AutoForm();

            var result = new MerchantStore();
            result.UserId = autoModel.BasicUser.Id;
            if (id == null)
            {
                var store = Resolve<IMerchantStoreService>().GetMerchantStore(autoModel.BasicUser.Id);
                if (store != null) {
                    result = store.FirstOrDefault();
                }
            }
            else
            {
                var store = Resolve<IMerchantStoreService>().GetMerchantStore(id.ConvertToLong());
                if (store != null) {
                    result = store.FirstOrDefault();
                }
            }

            if (result == null) {
                result = new MerchantStore();
            }

            autoForm = ToAutoForm(result);

            autoForm.AlertText = "【店铺设置】主要针对于店铺名称、Logo、描述的设置。";
            autoForm.ButtomHelpText = new List<string>
            {
                "店铺名称：用于标识店铺主要信息；",
                "店铺Logo：主要用于上传图片，标识店铺图标",
                "店铺描述：对店铺进行详细性的信息描述"
            };

            return autoForm;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var mapInstance = model.MapTo<MerchantStoreAutoForm>(); //商店的表單
            var instance = model.MapTo<MerchantStore>() ?? new MerchantStore();
            try
            {
                //已经存在的店铺
                if (mapInstance.Id.ToObjectId() != ObjectId.Empty)
                {
                    //instance.Id= mapInstance.Id.ToObjectId();
                    var merchantStore = Resolve<IMerchantStoreService>()
                        .GetSingle(x => x.Id == mapInstance.Id.ToObjectId());
                    if (merchantStore != null) {
                        instance.Id = merchantStore.Id;
                    }
                }
                else
                {
                    //不存在就新增
                    instance.UserId = autoModel.BasicUser.Id;
                }

                instance.Name = mapInstance.Name;
                instance.Logo = mapInstance.Logo;
                instance.Description = mapInstance.Description;
                var rs = Resolve<IMerchantStoreService>().AddOrUpdate(instance);
                return ServiceResult.Success;
            }
            catch (Exception exc)
            {
                return new ServiceResult(false, new List<string> {$"发生错误, {exc.Message}!"});
            }
        }
    }
}