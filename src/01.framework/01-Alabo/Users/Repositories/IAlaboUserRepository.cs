using Alabo.Domains.Repositories;

namespace Alabo.Users.Repositories {

    public interface IAlaboUserRepository : IRepository<Entities.User, long> {
    }
}