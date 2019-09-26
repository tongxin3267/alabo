using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Dtos;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Services {

    /// <summary>
    ///     用户地址管理
    /// </summary>
    public interface IUserAddressService : IService<UserAddress, ObjectId> {
        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="userAddress"></param>

        ServiceResult AddOrUpdateSingle(UserAddress userAddress);

        /// <summary>
        /// 修改或添加用户备案地址
        /// </summary>
        /// <param name="userInfoAddress"></param>

        ServiceResult SaveUserInfoAddress(UserInfoAddressInput userInfoAddress);

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="addressInput"></param>

        ServiceResult SaveOrderAddress(AddressInput addressInput);

        /// <summary>
        ///     删除用户地址
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        ServiceResult Delete(long userId, ObjectId id);

        /// <summary>
        ///     获取用户所有地址
        /// </summary>
        /// <param name="userId">会员Id</param>
        IList<UserAddress> GetAllList(long userId);

        /// <summary>
        ///     获取s the 会员 address.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId">会员Id</param>
        UserAddress GetUserAddress(ObjectId id, long userId);

        /// <summary>
        /// 获取用户备案地址
        /// </summary>
        /// <param name="userId"></param>

        UserAddress GetUserInfoAddress(long userId);

        /// <summary>
        ///     设为默认地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="addressId"></param>
        ServiceResult SetDefault(long userId, ObjectId addressId);

        /// <summary>
        /// 前台vant地址格式数据
        /// </summary>

        VantAddress GetVantAddress();
    }
}