using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Repositories
{
    public class DeliveryTemplateRepository : RepositoryMongo<DeliveryTemplate, ObjectId>, IDeliveryTemplateRepository
    {
        public DeliveryTemplateRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}