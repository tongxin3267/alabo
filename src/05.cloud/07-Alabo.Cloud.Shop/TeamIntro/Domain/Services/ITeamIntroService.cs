using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.TeamIntro.Domain.Services {

    public interface ITeamIntroService : IService<Entities.TeamIntro, ObjectId> {
        Entities.TeamIntro GetCourseView(object id);

        ServiceResult AddOrUpdate(Entities.TeamIntro view);
    }
}
