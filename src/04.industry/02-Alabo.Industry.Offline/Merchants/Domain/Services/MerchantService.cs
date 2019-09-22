using MongoDB.Bson;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Merchants.Domain.Services
{
    public class MerchantService : ServiceBase<Merchant, ObjectId>, IMerchantService
    {
        public MerchantService(IUnitOfWork unitOfWork, IRepository<Merchant, ObjectId> repository) : base(unitOfWork, repository)
        {
        }
    }
}