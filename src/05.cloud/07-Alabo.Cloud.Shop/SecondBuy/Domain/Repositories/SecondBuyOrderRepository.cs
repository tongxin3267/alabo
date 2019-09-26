using Alabo.Cloud.Shop.SecondBuy.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SecondBuy.Domain.Repositories
{
    public class SecondBuyOrderRepository : RepositoryMongo<SecondBuyOrder, ObjectId>, ISecondBuyOrderRepository
    {
        public SecondBuyOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}