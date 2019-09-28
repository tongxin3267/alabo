using System.Collections.Generic;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services
{
    public class DeliveryTemplateService : ServiceBase<DeliveryTemplate, ObjectId>, IDeliveryTemplateService
    {
        public DeliveryTemplateService(IUnitOfWork unitOfWork, IRepository<DeliveryTemplate, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }

        /// <summary>
        ///     获取运费模板
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public List<KeyValue> GetStoreDeliveryTemplateByCache(ObjectId StoreId)
        {
            var store = Resolve<IShopStoreService>().GetSingleFromCache(storeId);
            if (store == null) return null;
            var result = new List<KeyValue>();
            var list = GetList(r => r.StoreId == storeId);
            list.Foreach(r => { result.Add(new KeyValue(r.Id, r.TemplateName)); });
            return result;
        }
    }
}