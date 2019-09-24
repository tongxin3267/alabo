using MongoDB.Bson;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Market.SuccessfulCases.Domains.Services {

    public interface ICasesService : IService<Cases, ObjectId> {

        Cases GetCourseView(object id);

        ServiceResult AddOrUpdate(Cases view);
    }
}
