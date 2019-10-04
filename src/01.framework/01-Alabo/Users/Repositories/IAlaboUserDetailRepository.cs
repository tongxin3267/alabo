using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Users.Repositories {

    public interface IAlaboUserDetailRepository : IRepository<UserDetail, long> {
    }
}