using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    public interface IDeliverRepository : IRepository<Deliver, ObjectId>
    {
    }
}