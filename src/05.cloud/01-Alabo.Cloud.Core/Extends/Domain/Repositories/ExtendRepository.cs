using _01_Alabo.Cloud.Core.Extends.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.Extends.Domain.Repositories {

    public class ExtendRepository : RepositoryMongo<Extend, ObjectId>, IExtendRepository {

        public ExtendRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}