using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Users.Repositories
{
    public class UserRepository : RepositoryEfCore<User, long>, IAlaboUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}