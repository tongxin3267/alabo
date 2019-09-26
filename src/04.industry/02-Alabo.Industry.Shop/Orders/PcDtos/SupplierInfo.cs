using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AutoMapper;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.PcDtos {

    [ClassProperty(Name = "资料完善")]
    public class SupplierInfo : UIBase, IAutoForm {

        /// <summary>
        ///     供应商名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public string Name { get; set; }

        /// <summary>
        ///     联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public string Moblie { get; set; }

        /// <summary>
        ///     银行卡
        /// </summary>
        [Display(Name = "银行卡")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public string BankCard { get; set; }

        public long UserId { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            var result = ToAutoForm(new SupplierInfo());
            result.AlertText = "【完善资料】可以在此处完善您的资料，建议您填写正确资料";

            result.ButtomHelpText = new List<string> {
                "建议店铺名字不超过10个字！",
                "建议填写您的手机号码，便于通知发货！",
            };

            var view = Resolve<IShopStoreService>().GetSingle(u => u.UserId == autoModel.BasicUser.Id);
            if (view != null) {
                var user = Resolve<IUserService>().GetSingle(u => u.Id == view.UserId);
                var info = AutoMapping.SetValue<SupplierInfo>(view);

                if (!view.Extension.IsNullOrEmpty()) {
                    var storeExtension = view.Extension.ToObject<StoreExtension>();
                    info.BankCard = storeExtension?.BankCard;
                }

                info.Moblie = user.Mobile;
                var infoResult = ToAutoForm(info);

                infoResult.AlertText = "【完善资料】可以在此处完善您的资料，建议您填写正确资料";

                infoResult.ButtomHelpText = new List<string> {
                    "建议店铺名字不超过10个字！",
                    "建议填写您的手机号码，便于通知发货！",
                };
                return infoResult;
            }

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = (SupplierInfo)model;
            if (view.UserId <= 0) {
                return ServiceResult.FailedWithMessage("不存在的会员");
            }

            var store = Resolve<IShopStoreService>().GetSingle(u => u.UserId == view.UserId);
            if (store == null) {
                return ServiceResult.FailedWithMessage("该用户不是供应商");
            }

            var storeExtension = new StoreExtension();
            storeExtension.BankCard = view.BankCard;
            store.Name = view.Name;

            var updateRes = Resolve<IShopStoreService>().Update(store);
            if (!updateRes) {
                return ServiceResult.FailedWithMessage("修改失败 请重试");
            }
            if (store.Extension.IsNullOrEmpty()) {
                store.Extension = ObjectExtension.ToJson(storeExtension);
            }
            var updateExtension = Resolve<IShopStoreService>().UpdateExtensions(store.Id, storeExtension);

            if (!view.Moblie.IsNullOrEmpty()) {
                var viewUser = Resolve<IUserService>().GetSingle(u => u.Id == view.UserId);
                viewUser.Mobile = view.Moblie;
                Resolve<IUserService>().Update(viewUser);
            }
            return ServiceResult.Success;
        }
    }
}