using Alabo.Cloud.Shop.SuccessfulCases.Domains.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SuccessfulCases.Domains.Services {

    public interface ICasesService : IService<Cases, ObjectId> {

        Cases GetCourseView(object id);

        ServiceResult AddOrUpdate(Cases view);
    }
}
