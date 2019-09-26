using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Services
{
    public class ParentInternalService : ServiceBase<ParentInternal, ObjectId>, IParentInternalService
    {
        public ParentInternalService(IUnitOfWork unitOfWork, IRepository<ParentInternal, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}