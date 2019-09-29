using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Merchants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Merchants.Domain.Repositories
{
    public class MerchantRepository : RepositoryMongo<Merchant, ObjectId>, IMerchantRepository
    {
        public MerchantRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}