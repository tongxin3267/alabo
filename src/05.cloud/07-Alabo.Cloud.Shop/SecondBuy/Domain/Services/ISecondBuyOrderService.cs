using System.Collections.Generic;
using Alabo.Cloud.Shop.SecondBuy.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SecondBuy.Domain.Services
{
    public interface ISecondBuyOrderService : IService<SecondBuyOrder, ObjectId>
    {
        /// <summary>
        ///     ����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        ServiceResult Buy(SecondBuyOrder order);

        /// <summary>
        ///     �����б�
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<string> BuyList(long productId);


        /// <summary>
        ///     �������
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<string> BuyListRcently(long productId);
    }
}