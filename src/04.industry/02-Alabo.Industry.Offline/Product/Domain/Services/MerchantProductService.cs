using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.Product.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Product.Domain.Services
{
    public class MerchantProductService : ServiceBase<MerchantProduct, ObjectId>, IMerchantProductService
    {
        public MerchantProductService(IUnitOfWork unitOfWork, IRepository<MerchantProduct, ObjectId> repository)
            : base(unitOfWork, repository)
        {
        }
    }
}