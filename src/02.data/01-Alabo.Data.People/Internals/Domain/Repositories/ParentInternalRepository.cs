using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Repositories {

    public class ParentInternalRepository : RepositoryMongo<ParentInternal, ObjectId>, IParentInternalRepository {

        public ParentInternalRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}