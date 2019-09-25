using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.Users.Repositories {

    public class UserRepository : RepositoryEfCore<Entities.User, long>, IAlaboUserRepository {

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}