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
        /// ��ȡȨ���޸���ͼ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserRights GetEditView(object id);

        /// <summary>
        /// ��ӻ�ɾ��Ȩ��
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult AddOrUpdate(UserRights view);

        /// <summary>
        ///     ��ȡ�̼�Ȩ��
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<UserRightsOutput> GetView(long userId);
        /// <summary>
        ///     ��ȡ�̼�Ȩ��
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        IList<UserRightsOutput> GetView(bool isAdmin);
        /// <summary>
        ///     �̼ҷ��񶩹�
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> Buy(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     ��ȡ֧���ļ۸�
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, decimal> GetPayPrice(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     ����˿�ͨ
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> OpenToOther(UserRightsOrderInput orderBuyInput);

        /// <summary>
        ///     �Լ���ͨ���Լ�����
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        Task<Tuple<ServiceResult, OrderBuyOutput>> OpenSelfOrUpgrade(UserRightsOrderInput orderBuyInput);

        /// <summary>
        /// ֧��ʱ����ִ�е�Sql�ű�
        /// </summary>
        /// <param name="entityIdList"></param>
        /// <returns></returns>
        List<string> ExcecuteSqlList(List<object> entityIdList);

        /// <summary>
        /// ֧���ɹ��ص�����
        /// </summary>
        /// <param name="entityIdList"></param>
        void AfterPaySuccess(List<object> entityIdList);
    }
}