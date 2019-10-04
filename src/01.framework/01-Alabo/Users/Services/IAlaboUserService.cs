using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Users.Services {

    /// <summary>
    ///     用户操作相关方法
    /// </summary>
    public interface IAlaboUserService : IService<User, long> {
    }
}