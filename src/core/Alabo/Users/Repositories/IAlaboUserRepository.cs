using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IAlaboUserRepository : IRepository<Entities.User, long> {
    }
}