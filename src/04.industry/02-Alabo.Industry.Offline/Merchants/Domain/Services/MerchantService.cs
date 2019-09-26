using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Services
{
    public class MerchantService : ServiceBase<Merchant, ObjectId>, IMerchantService
    {
        public MerchantService(IUnitOfWork unitOfWork, IRepository<Merchant, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}