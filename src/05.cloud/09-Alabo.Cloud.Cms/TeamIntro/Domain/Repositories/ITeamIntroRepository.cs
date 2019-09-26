using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.TeamIntro.Domain.Repositories {

    public interface ITeamIntroRepository : IRepository<Entities.TeamIntro, ObjectId> {
    }
}