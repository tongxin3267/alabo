using MongoDB.Bson;
using Alabo.App.Market.PromotionalMaterial.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.PromotionalMaterial.Domain.Repositories {

    public interface IMaterialRepository : IRepository<Material, ObjectId> {
    }
}