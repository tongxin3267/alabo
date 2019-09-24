using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.SecondBuy.Domain.Entities;
using Alabo.Domains.Entities;
using System.Collections.Generic;

namespace Alabo.App.Market.SecondBuy.Domain.Services {
	public interface ISecondBuyOrderService : IService<SecondBuyOrder, ObjectId>  {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        ServiceResult Buy(SecondBuyOrder order);

        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<string> BuyList(long productId);


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<string> BuyListRcently(long productId);
    }
	}
