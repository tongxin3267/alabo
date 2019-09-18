using System;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// </summary>
    public class UserAdminService : ServiceBase, IUserAdminService {

        /// <summary>
        ///     The single 会员 cache key
        /// </summary>
        private static readonly string _singleUserCacheKey = "SingleUserCacheKey";

        /// <summary>
        ///     The 会员 map repository
        /// </summary>
        private readonly IUserMapRepository _userMapRepository;

        /// <summary>
        ///     The 会员 repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService" /> class.
        /// </summary>
        public UserAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
            _userRepository = Repository<IUserRepository>();
            _userMapRepository = Repository<IUserMapRepository>();
        }

        /// <summary>
        ///   物理删除会员  删除用户   直推下级的直接推荐人 修改当前会员的推荐人。
        /// </summary>
        /// <param name="userId">用户Id</param>
        public bool DeleteUser(long userId) {
            var model = Resolve<IUserService>().GetUserDetail(userId);

            if (!_userRepository.Delete(userId)) {
                return false;
            } else {
                // 删除成功以后
                Resolve<IUserService>().DeleteUserCache(model.Id, model.UserName);
                var userConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();

                // 删除会员后修改推荐人，下面所有的会员修改组织架构图
                if (userConfig.AfterDeleteUserUpdateParentMap) {
                    Resolve<IUserMapService>().UpdateParentUserAfterUserDelete(userId, model.ParentId);
                }
            }
            return true;
        }

        /// <summary>
        ///     用户表更新 user_user 缓存删除
        /// </summary>
        /// <param name="user">用户</param>
        public ServiceResult UpdateUser(Entities.User user) {
            try {
                Resolve<IUserService>().Update(user);
                Resolve<IUserService>().DeleteUserCache(user.Id, user.UserName);
                return ServiceResult.Success;
            } catch (Exception ex) {
                return ServiceResult.FailedMessage(ex.Message);
            }
        }

        /// <summary>
        ///     用户表更新 user_userDetail 缓存删除
        /// </summary>
        /// <param name="userDetail"></param>
        public bool UpdateUserDetail(UserDetail userDetail) {
            try {
                Resolve<IUserDetailService>().Update(userDetail);
                Resolve<IUserService>().DeleteUserCache(userDetail.UserId);
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}