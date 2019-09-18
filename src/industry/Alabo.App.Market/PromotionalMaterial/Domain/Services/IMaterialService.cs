using MongoDB.Bson;
using Alabo.App.Market.PromotionalMaterial.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Market.PromotionalMaterial.Domain.Services {

    public interface IMaterialService : IService<Material, ObjectId> {

        Material GetCourseView(object id);

        ServiceResult AddOrUpdate(Material view);
    }
}
