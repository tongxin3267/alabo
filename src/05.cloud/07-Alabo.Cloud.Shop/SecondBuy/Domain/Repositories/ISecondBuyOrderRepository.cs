using Alabo.Cloud.Shop.SecondBuy.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SecondBuy.Domain.Repositories
{
    public interface ISecondBuyOrderRepository : IRepository<SecondBuyOrder, ObjectId>
    {
    }
}