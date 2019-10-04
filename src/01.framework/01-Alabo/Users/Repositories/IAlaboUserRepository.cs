using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Users.Repositories {

    public interface IAlaboUserRepository : IRepository<User, long> {
    }
}