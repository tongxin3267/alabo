using Alabo.Domains.Services;

namespace Alabo.Users.Services {

    /// <summary>
    ///     用户操作相关方法
    /// </summary>
    public interface IAlaboUserService : IService<Entities.User, long> {
    }
}