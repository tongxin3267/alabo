using MongoDB.Bson;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Order.Domain.Repositories
{
    public class MerchantCardRepository : RepositoryMongo<MerchantCart, ObjectId>, IMerchantCartRepository
    {
        public MerchantCardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}