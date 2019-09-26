using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.TeamIntro.Domain.Repositories {

    public class TeamIntroRepository : RepositoryMongo<Entities.TeamIntro, ObjectId>, ITeamIntroRepository {

        public TeamIntroRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        
    }
}