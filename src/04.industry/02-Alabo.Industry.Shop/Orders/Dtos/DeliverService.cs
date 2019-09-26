using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    public class DeliverService : ServiceBase<Deliver, ObjectId>, IDeliverService
    {
        public DeliverService(IUnitOfWork unitOfWork, IRepository<Deliver, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}