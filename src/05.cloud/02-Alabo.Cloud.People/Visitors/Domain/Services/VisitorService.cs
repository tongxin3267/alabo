using Alabo.Cloud.People.Visitors.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Visitors.Domain.Services
{
    public class VisitorService : ServiceBase<Visitor, ObjectId>, IVisitorService
    {
        public VisitorService(IUnitOfWork unitOfWork, IRepository<Visitor, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}