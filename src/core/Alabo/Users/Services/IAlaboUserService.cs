using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     用户操作相关方法
    /// </summary>
    public interface IAlaboUserService : IService<Entities.User, long> {
    }
}