using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Attach.Domain.Repositories {

    public class ExtendRepository : RepositoryMongo<Extend, ObjectId>, IExtendRepository {

        public ExtendRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}