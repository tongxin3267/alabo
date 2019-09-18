using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class UserQrCodeRepository : RepositoryEfCore<UserDetail, long>, IUserQrCodeRepository {

        public UserQrCodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}