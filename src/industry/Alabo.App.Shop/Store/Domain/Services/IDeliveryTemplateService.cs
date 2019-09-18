using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.Store.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Store.Domain.Services
{
    public interface IDeliveryTemplateService : IService<DeliveryTemplate, ObjectId>
    {

        /// <summary>
        /// 获取运费模板
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        List<KeyValue> GetStoreDeliveryTemplateByCache(long storeId);

    }

}
