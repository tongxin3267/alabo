using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Repositories
{
    public class InternalRepository : RepositoryMongo<Internal, ObjectId>, IInternalRepository
    {
        public InternalRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}