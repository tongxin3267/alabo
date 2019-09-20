using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     后台用户操作接口
    /// </summary>
    public interface IUserAdminService : IService {

        ServiceResult UpdateUser(Entities.User user);

        bool UpdateUserDetail(UserDetail userDetail);

        /// <summary>
        ///     物理删除会员 改变推荐关系
        /// </summary>
        /// <param name="userId">用户Id</param>
        bool DeleteUser(long userId);
    }
}