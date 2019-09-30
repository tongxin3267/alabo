using Alabo.Cloud.Asset.SecondBuy.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Cloud.Asset.SecondBuy.Domain.Services
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