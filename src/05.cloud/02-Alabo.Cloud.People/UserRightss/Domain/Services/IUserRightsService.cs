using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Cloud.People.UserRightss.Domain.Dtos;
using Alabo.Cloud.People.UserRightss.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Orders.Dtos;

namespace Alabo.Cloud.People.UserRightss.Domain.Services
{

    public interface IUserRightsService : IService<UserRights, long>
    {

        /// <summary>
        /// 获取权益修改视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserRights GetEditView(object id);

        /// <summary>
        /// 添加或删除权益
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult AddOrUpdate(UserRights view);

        /// <summary>
        ///     获取商家权益
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<UserRightsOutput> GetView(long userId);
        /// <summary>
        ///     获取商家权益
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        IList<UserRightsOutput> GetView(bool isAdmin);
        /// <summary>
        ///     商家服务订购
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> Buy(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     获取支付的价格
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, decimal> GetPayPrice(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     帮别人开通
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> OpenToOther(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     自己开通或自己升级
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> OpenSelfOrUpgrade(UserRightsOrderInput orderBuyInput);

        /// <summary>
        /// 支付时调用执行的Sql脚本
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        List<string> ExcecuteSqlList(List<object> entityIdList);

        /// <summary>
        /// 支付成功回调函数
        /// </summary>
        /// <param name="entityIdList"></param>
        void AfterPaySuccess(List<object> entityIdList);
    }
}