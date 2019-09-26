using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Users.Services
{
    /// <summary>
    ///     Class UserService.
    /// </summary>
    public class AlaboUserService : ServiceBase<User, long>, IAlaboUserService
    {
        public AlaboUserService(IUnitOfWork unitOfWork, IRepository<User, long> repository) : base(unitOfWork,
            repository)
        {
            ;
        }
    }
}