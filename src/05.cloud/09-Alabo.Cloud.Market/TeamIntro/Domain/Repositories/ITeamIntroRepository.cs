using MongoDB.Bson;
using Alabo.App.Market.TeamIntro.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.TeamIntro.Domain.Repositories {

    public interface ITeamIntroRepository : IRepository<Entities.TeamIntro, ObjectId> {
    }
}