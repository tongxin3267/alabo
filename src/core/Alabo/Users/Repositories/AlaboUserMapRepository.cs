using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class UserMapRepository : RepositoryEfCore<UserMap, long>, IAlaboUserMapRepository {

        public UserMapRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}