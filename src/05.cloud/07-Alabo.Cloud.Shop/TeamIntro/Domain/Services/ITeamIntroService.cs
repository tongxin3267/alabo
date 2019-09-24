using MongoDB.Bson;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Market.TeamIntro.Domain.Services {

    public interface ITeamIntroService : IService<Entities.TeamIntro, ObjectId> {
        Entities.TeamIntro GetCourseView(object id);

        ServiceResult AddOrUpdate(Entities.TeamIntro view);
    }
}
