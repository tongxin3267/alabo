using MongoDB.Bson;
using Alabo.App.Market.PromotionalMaterial.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.PromotionalMaterial.Domain.Repositories {

    public class MaterialRepository : RepositoryMongo<Material, ObjectId>, IMaterialRepository {

        public MaterialRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}