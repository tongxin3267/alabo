using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Store.Domain.Entities;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Extensions;

namespace Alabo.App.Shop.Store.Domain.Services
{
    public class DeliveryTemplateService : ServiceBase<DeliveryTemplate, ObjectId>, IDeliveryTemplateService
    {
        public DeliveryTemplateService(IUnitOfWork unitOfWork, IRepository<DeliveryTemplate, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }

        /// <summary>
        /// 获取运费模板
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public List<KeyValue> GetStoreDeliveryTemplateByCache(long storeId)
        {
            var store = Resolve<IStoreService>().GetSingleFromCache(storeId);
            if (store == null)
            {
                return null;
            }
            var result = new List<KeyValue>();
            var list = GetList(r => r.StoreId == storeId);
            list.Foreach(r => { result.Add(new KeyValue(r.Id, r.TemplateName)); });
            return result;
        }
    }
}
