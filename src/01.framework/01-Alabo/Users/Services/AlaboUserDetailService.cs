using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Domain.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public class AlaboUserDetailService : ServiceBase<UserDetail, long>, IAlaboUserDetailService {

        public AlaboUserDetailService(IUnitOfWork unitOfWork, IRepository<UserDetail, long> repository) : base(unitOfWork,
            repository) {
        }
    }
}