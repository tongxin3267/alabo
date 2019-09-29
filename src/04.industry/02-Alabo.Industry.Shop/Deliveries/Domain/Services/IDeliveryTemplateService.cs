using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services
{
    public interface IDeliveryTemplateService : IService<DeliveryTemplate, ObjectId>
    {
        /// <summary>
        ///     ��ȡ�˷�ģ��
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        List<KeyValue> GetStoreDeliveryTemplateByCache(ObjectId StoreId);
    }
}