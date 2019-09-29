using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Repositories
{
    public interface IDeliveryTemplateRepository : IRepository<DeliveryTemplate, ObjectId>
    {
    }
}