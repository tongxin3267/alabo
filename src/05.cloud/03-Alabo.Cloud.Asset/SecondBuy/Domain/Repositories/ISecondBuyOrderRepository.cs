using Alabo.Cloud.Asset.SecondBuy.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Asset.SecondBuy.Domain.Repositories
{
    public interface ISecondBuyOrderRepository : IRepository<SecondBuyOrder, ObjectId>
    {
    }
}