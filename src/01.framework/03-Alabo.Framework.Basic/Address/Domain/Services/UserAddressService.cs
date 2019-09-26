using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Mapping;
using Alabo.Users.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     地址数据保存到UserDetail表中的 Extensions 对象中
    ///     为UserExtensions对象
    /// </summary>
    public class UserAddressService : ServiceBase<UserAddress, ObjectId>, IUserAddressService {

        public UserAddressService(IUnitOfWork unitOfWork, IRepository<UserAddress, ObjectId> repository) : base(
            unitOfWork, repository) {
        }

        /// <summary>
        ///     删除用户地址
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        public ServiceResult Delete(long userId, ObjectId id) {
            var find = GetSingle(r => r.Id == id && r.UserId == userId);
            if (find == null) {
                return ServiceResult.FailedWithMessage("地址不存在");
            }
            //else {
            //    return ServiceResult.FailedWithMessage("已添加的地址不允许删除");
            //}

            var result = Delete(find);
            if (result) {
                InitDefaultAddress(userId);
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedMessage("删除失败");
            }
        }

        /// <summary>
        ///     Gets the user address.
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="userId">The user identifier.</param>
        public UserAddress GetUserAddress(ObjectId guid, long userId) {
            InitDefaultAddress(userId);
            if (guid.IsObjectIdNullOrEmpty()) {
                return GetSingle(r => r.UserId == userId && r.IsDefault == true);
            }
            return GetSingle(r => r.UserId == userId && r.Id == guid);
        }

        /// <summary>
        ///     Sets the default.
        ///     设为默认地址
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="addressId">The address identifier.</param>
        public ServiceResult SetDefault(long userId, ObjectId addressId) {
            var userAddress = GetList(r => r.UserId == userId);
            if (userAddress != null && userAddress.Count > 0) {
                var address = userAddress.FirstOrDefault(r => r.Id == addressId);
                if (address == null) {
                    return ServiceResult.FailedWithMessage("地址未找到，或已删除");
                }

                address.IsDefault = true;
                if (Update(address)) {
                    userAddress.Remove(address);
                    userAddress.Foreach(u => {
                        u.IsDefault = false;
                        Update(u);
                    });
                    return ServiceResult.Success;
                }

                //// 修改当前的默认地址为true
                //if (temp) {
                //    // 修改其他的地址为false
                //    if (Update(r => r.IsDefault = false, r => r.Id != addressId && r.UserId == userId)) {
                //        return ServiceResult.Success;
                //    }
                //}
            }

            return ServiceResult.FailedWithMessage("默认地址修改失败");
        }

        /// <summary>
        ///     Initializes the default address.
        ///     初始化默认地址，如果默认地址不存在的时候，设置为第一个
        /// </summary>
        /// <param name="userId">会员Id</param>
        private void InitDefaultAddress(long userId) {
            var address = GetList(r => r.UserId == userId);
            if (address != null) {
                var defaultAddress = address.FirstOrDefault(r => r.IsDefault);
                // 不存在默认地址
                if (defaultAddress == null) {
                    var firsetAddress = address.FirstOrDefault(); // 使用第一个做默认地址
                    if (firsetAddress != null) {
                        SetDefault(userId, firsetAddress.Id);
                    }
                }
            }
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="userInfoAddress"></param>

        public ServiceResult SaveUserInfoAddress(UserInfoAddressInput userInfoAddress) {
            if (userInfoAddress == null) {
                throw new System.Exception("输入不能为空");
            }
            var userAddress = AutoMapping.SetValue<UserAddress>(userInfoAddress);
            if (!userInfoAddress.Id.IsNullOrEmpty()) {
                userAddress.Id = userInfoAddress.Id.ToSafeObjectId();
            }
            var result = AddOrUpdateSingle(userAddress);
            if (result == ServiceResult.Success) {
                // 修改User_Detial表
                var userDetail = Resolve<IAlaboUserDetailService>().GetSingle(r => r.UserId == userInfoAddress.UserId);
                userDetail.AddressId = userAddress.Id.ToStr();
                userDetail.RegionId = userInfoAddress.RegionId;
                Resolve<IAlaboUserDetailService>().Update(userDetail);
            }

            return result;
        }

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="addressInput"></param>

        public ServiceResult SaveOrderAddress(AddressInput addressInput) {

            #region MyRegion

            //if (addressInput == null) {
            //    throw new System.Exception("输入不能为空");
            //}

            //var userAddress = AutoMapping.SetValue<UserAddress>(addressInput);
            //if (!addressInput.Id.IsNullOrEmpty()) {
            //    userAddress.Id = addressInput.Id.ToObjectId();
            //} else {
            //    var addressList = Resolve<IUserAddressService>().GetList(u => u.UserId == addressInput.UserId);
            //    if (addressList.Count >= 1) {
            //        return ServiceResult.FailedWithMessage("每个用户只允许拥有一个收货地址");
            //    }
            //}

            //userAddress.Type = AddressLockType.OrderAddress;
            //return AddOrUpdateSingle(userAddress);

            #endregion MyRegion

            #region 5.20后开放

            //不允许更改收货地址
            //只允许更改收货人姓名及电话
            //限定每个用户只能有一个地址

            if (addressInput == null) {
                throw new System.Exception("输入不能为空");
            }
            var userAddress = AutoMapping.SetValue<UserAddress>(addressInput);
            var addressConfig = Resolve<IAutoConfigService>().GetValue<UserAddressConfig>();
            if (!addressInput.Id.IsNullOrEmpty() && !addressInput.Id.ToObjectId().IsObjectIdNullOrEmpty()) {
                //修改
                userAddress.Id = addressInput.Id.ToObjectId();
                var model = Resolve<IUserAddressService>().GetSingle(u => u.Id == addressInput.Id.ToObjectId());
                if (model == null) {
                    return ServiceResult.FailedWithMessage("数据异常");
                }

                //查看配置是否开启
                if (addressConfig.IsEnble) {
                    if (userAddress.Address != model.Address) {
                        //不允许修改
                        return ServiceResult.FailedWithMessage(addressConfig.EditTips);
                    }
                    if (userAddress.RegionId != model.RegionId) {
                        //不允许修改
                        return ServiceResult.FailedWithMessage(addressConfig.EditTips);
                    }
                }
            } else {
                //限定每个用户只能有一个地址
                //var addressList = Resolve<IUserAddressService>().GetList(u => u.UserId == addressInput.UserId);
                //if (addressList.Count >= 1)
                //{
                //    return ServiceResult.FailedWithMessage("每个用户只允许拥有一个收货地址");
                //}
                //新增
                if (addressConfig.IsEnble) {
                    var addressList = Resolve<IUserAddressService>().GetList(u => u.UserId == addressInput.UserId);
                    if (addressList.Count >= addressConfig.MaxNumber) {
                        return ServiceResult.FailedWithMessage(addressConfig.AddTips);
                    }
                }
            }

            userAddress.Id = addressInput.Id.ToObjectId();
            userAddress.Type = AddressLockType.OrderAddress;

            return AddOrUpdateSingle(userAddress);

            #endregion 5.20后开放
        }

        public IList<UserAddress> GetAllList(long userId) {
            var model = GetList(r => r.UserId == userId);
            foreach (var item in model) {
                item.AddressDescription = Resolve<IRegionService>().GetRegionNameById(item.RegionId);
            }

            return model;
        }

        public UserAddress GetUserInfoAddress(long userId) {
            return GetSingle(r => r.UserId == userId && r.Type == AddressLockType.UserInfoAddress);
        }

        /// <summary>
        /// 添加或更新地址
        /// </summary>
        /// <param name="userAddress"></param>

        public ServiceResult AddOrUpdateSingle(UserAddress userAddress) {
            if (userAddress == null) {
                throw new ValidException("地址不能为空");
            }
            var find = GetSingle(userAddress.Id);
            if (userAddress.Type == AddressLockType.UserInfoAddress) {
                find = GetSingle(u => u.UserId == userAddress.UserId && u.Type == userAddress.Type);
            }

            if (find == null) {
                find = new UserAddress();
            }

            var user = Resolve<IAlaboUserService>().GetSingle(r => r.Id == userAddress.UserId);

            if (user == null) {
                return ServiceResult.FailedWithMessage("会员不存在");
            }

            var region = Resolve<IRegionService>().GetSingle(r => r.RegionId == userAddress.RegionId);
            if (region == null) {
                return ServiceResult.FailedWithMessage("您选择的区域不存在");
            }

            if (region.Level != RegionLevel.County) {
                return ServiceResult.FailedWithMessage("请选择完整的地址");
            }

            find.AddressDescription = region.FullName + " " + userAddress.Address;
            find.Address = userAddress.Address;
            find.Type = userAddress.Type;
            find.City = region.CityId;
            find.Province = region.ProvinceId;
            find.IsDefault = userAddress.IsDefault;

            find.ZipCode = userAddress.ZipCode;
            find.UserId = user.Id;
            find.RegionId = region.RegionId;

            find.Mobile = userAddress.Mobile;
            find.Name = userAddress.Name;

            //名字和手机特殊处理
            if (find.Mobile.IsNullOrEmpty()) {
                find.Mobile = user.Mobile;
            }
            if (find.Name.IsNullOrEmpty()) {
                find.Name = user.Name;
            }
            if (find.Name.IsNullOrEmpty()) {
                find.Name = user.UserName;
            }

            var saveResult = AddOrUpdate(find, r => r.Id == find.Id);
            if (saveResult && userAddress.IsDefault) {
                return SetDefault(userAddress.UserId, find.Id); // 如果设置为默认值地址，则更新为默认值地址
            } else if (saveResult && !userAddress.IsDefault) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("地址保存失败");
        }

        public VantAddress GetVantAddress() {
            var cacheKey = "GetVantAddress";
            return ObjectCache.GetOrSet(() => {
                var regionList = Resolve<IRegionService>().GetList();
                var result = new VantAddress();
                // 省份
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (var item in regionList.Where(r => r.Level == RegionLevel.Province)) {
                    dictionary.Add(item.RegionId.ToString(), item.Name);
                }

                result.province_list = dictionary;

                //城市
                dictionary = new Dictionary<string, string>();
                foreach (var item in regionList.Where(r => r.Level == RegionLevel.City)) {
                    dictionary.Add(item.RegionId.ToString(), item.Name);
                }

                result.city_list = dictionary;

                //区县
                dictionary = new Dictionary<string, string>();
                foreach (var item in regionList.Where(r => r.Level == RegionLevel.County)) {
                    dictionary.Add(item.RegionId.ToString(), item.Name);
                }

                result.county_list = dictionary;

                return result;
            }, cacheKey).Value;
        }
    }
}