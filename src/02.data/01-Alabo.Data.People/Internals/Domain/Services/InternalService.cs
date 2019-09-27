using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Services
{
    public class InternalService : ServiceBase<Internal, ObjectId>, IInternalService
    {
        public InternalService(IUnitOfWork unitOfWork, IRepository<Internal, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}