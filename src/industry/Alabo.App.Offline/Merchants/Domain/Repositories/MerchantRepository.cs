using MongoDB.Bson;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Merchants.Domain.Repositories
{
    public class MerchantRepository : RepositoryMongo<Merchant, ObjectId>, IMerchantRepository
    {
        public MerchantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}