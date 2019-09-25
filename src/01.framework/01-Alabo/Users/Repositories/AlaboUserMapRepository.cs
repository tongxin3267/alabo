using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Users.Repositories {

    public class UserMapRepository : RepositoryEfCore<UserMap, long>, IAlaboUserMapRepository {

        public UserMapRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}