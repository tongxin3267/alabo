using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.TeamIntro.Domain.Repositories
{
    public interface ITeamIntroRepository : IRepository<Entities.TeamIntro, ObjectId>
    {
    }
}