using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.TeamIntro.Domain.Repositories {

    public class TeamIntroRepository : RepositoryMongo<Entities.TeamIntro, ObjectId>, ITeamIntroRepository {

        public TeamIntroRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        
    }
}