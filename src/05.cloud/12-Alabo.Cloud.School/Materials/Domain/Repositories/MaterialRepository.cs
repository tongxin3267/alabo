using Alabo.Cloud.School.Materials.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.Materials.Domain.Repositories {

    public class MaterialRepository : RepositoryMongo<Material, ObjectId>, IMaterialRepository {

        public MaterialRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}