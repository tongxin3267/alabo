using Alabo.Cloud.School.Materials.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.Materials.Domain.Services {

    public interface IMaterialService : IService<Material, ObjectId> {

        Material GetCourseView(object id);

        ServiceResult AddOrUpdate(Material view);
    }
}
