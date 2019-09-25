using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.Users.Services {

    /// <summary>
    ///     Class UserService.
    /// </summary>
    public class AlaboUserService : ServiceBase<Entities.User, long>, IAlaboUserService {

        public AlaboUserService(IUnitOfWork unitOfWork, IRepository<Entities.User, long> repository) : base(unitOfWork,
            repository) {
            ;
        }
    }
}