using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Users.Services {

    public class AlaboUserDetailService : ServiceBase<UserDetail, long>, IAlaboUserDetailService {

        public AlaboUserDetailService(IUnitOfWork unitOfWork, IRepository<UserDetail, long> repository) : base(unitOfWork,
            repository) {
        }
    }
}