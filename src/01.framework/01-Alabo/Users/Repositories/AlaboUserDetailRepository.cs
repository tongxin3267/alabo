using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Users.Repositories {

    internal class UserDetailRepository : RepositoryEfCore<UserDetail, long>, IAlaboUserDetailRepository {

        public UserDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}