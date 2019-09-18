using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.UserRightss.Domain.Repositories {

    public class UserRightsRepository : RepositoryEfCore<UserRights, long>, IUserRightsRepository {

        public UserRightsRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}