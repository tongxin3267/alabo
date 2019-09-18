using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Datas.Stores;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoForms;

namespace Alabo.App.Market.Store.Dtos {

    public class StoreEdit : UIBase, IAutoForm {
        public string UserName { get; set; }

        public string Name { get; set; }

        public bool IsPlanform { get; set; }

        public string Mobile { get; set; }

        public string BankCard { get; set; }

        public string ParentUserName { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel) {
            if (id.ToInt64() > 0) {
                var view = Resolve<IStoreService>().GetSingle(u => u.Id == id.ToInt64());
                if (view == null) {
                    return ToAutoForm(new StoreList());
                }
                var user = Resolve<IUserService>().GetSingle(u => u.Id == view.UserId);
                var storeList = AutoMapping.SetValue<StoreList>(view);
                storeList.Mobile = user.Mobile;
                storeList.UserName = user.UserName;
                var storeExtension = view.Extension.DeserializeJson<StoreExtension>();
                storeList.BankCard = storeExtension?.BankCard;
                return ToAutoForm(storeList);
            }

            return ToAutoForm(new StoreList());
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel) {
            var view = (StoreEdit)model;
            //修改供应商信息
            var viewUser = Resolve<IUserService>().GetSingle(u => u.UserName == view.UserName);
            var storeView = Resolve<IStoreService>().GetSingle(u => u.UserId == viewUser.Id);
            if (storeView != null) {
                if (viewUser.UserName != view.UserName) {
                    return ServiceResult.FailedWithMessage("修改失败 供应商对应用户名不可修改");
                }

                var storeExtension = new StoreExtension();
                storeExtension.BankCard = view.BankCard;

                storeView.Name = view.Name;
                storeView.IsPlanform = view.IsPlanform;
                storeView.Status = UserTypeStatus.Success;
                storeView.GradeId = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f700");

                var updateRes = Resolve<IStoreService>().Update(storeView);

                if (storeView.Extension.IsNullOrEmpty()) {
                    storeView.Extension = ObjectExtension.ToJson(storeExtension);
                }
                var updateExtension = Resolve<IStoreService>().UpdateExtensions(storeView.Id, storeExtension);

                if (viewUser.Mobile != view.Mobile) {
                    viewUser.Mobile = view.Mobile;
                    Resolve<IUserService>().Update(viewUser);
                }

                if (!updateRes) {
                    return ServiceResult.FailedWithMessage("修改失败 请重试");
                }

                return ServiceResult.Success;
            }

            //根据传入的数据自动注册一个等级为免费会员的供应商账号
            var userDetail = new UserDetail {
                Password = "111111",
                PayPassword = "111111"
            };
            var parentUser = Resolve<IUserService>().GetSingle(u => u.UserName == view.ParentUserName);
            long parentId = 5005;//线上站点供应链上级 对应name为供应链的账号
            if (parentUser != null) {
                parentId = parentUser.Id;
            }

            #region 注册会员

            long userId = 0;
            if (viewUser == null) {
                var user = new User {
                    Map = new UserMap(),
                    UserName = view.UserName.Trim(),
                    Name = view.Name,
                    Detail = userDetail,
                    ParentId = parentId,
                    Email = view.UserName + "@qnn.com",
                    Mobile = view.Mobile,
                    GradeId = Guid.Parse("72BE65E6-3000-414D-972E-1A3D4A366000")
                };

                //var addUser = Resolve<IUserService>().AddUser(user);

                //var result = Ioc.Resolve<UserManager>().RegisterAsync(user, false);
                //if (!result.Result.Succeeded) {
                //    return ServiceResult.FailedWithMessage("用户添加失败 请重试");
                //}

                var addUser = Resolve<IUserService>().GetSingle(u => u.UserName == view.UserName.Trim());
                userId = addUser.Id;
                if (addUser == null) {
                    return ServiceResult.FailedWithMessage("用户添加失败 请重试");
                }
            } else {
                userId = viewUser.Id;
            }

            #endregion 注册会员

            //添加店铺信息
            var store = new Shop.Store.Domain.Entities.Store {
                UserId = userId,
                Name = view.Name,
                IsPlanform = view.IsPlanform,
                Status = UserTypeStatus.Success,
                GradeId = Guid.Parse("72be65e6-3a64-414d-972e-1a3d4a36f700"),
            };
            store.StoreExtension = new StoreExtension {
                BankCard = view.BankCard
            };
            store.Extension = store.StoreExtension.ToJson();
            Resolve<IStoreService>().Add(store);

            return ServiceResult.Success;
        }
    }
}