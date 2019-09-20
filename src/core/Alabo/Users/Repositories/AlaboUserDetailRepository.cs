using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    internal class UserDetailRepository : RepositoryEfCore<UserDetail, long>, IAlaboUserDetailRepository {

        public UserDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}