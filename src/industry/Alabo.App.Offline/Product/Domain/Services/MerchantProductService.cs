using MongoDB.Bson;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Product.Domain.Services
{
    public class MerchantProductService : ServiceBase<MerchantProduct, ObjectId>, IMerchantProductService
    {
        public MerchantProductService(IUnitOfWork unitOfWork, IRepository<MerchantProduct, ObjectId> repository) 
            : base(unitOfWork, repository)
        {
        }
    }
}