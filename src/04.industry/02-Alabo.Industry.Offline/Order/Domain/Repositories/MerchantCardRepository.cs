using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Order.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Offline.Order.Domain.Repositories
{
    public class MerchantCardRepository : RepositoryMongo<MerchantCart, ObjectId>, IMerchantCartRepository
    {
        public MerchantCardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}