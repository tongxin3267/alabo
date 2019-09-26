using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    ///     Class UserService.
    /// </summary>
    public class UserService : ServiceBase<Users.Entities.User, long>, IUserService {

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

        public UserService(IUnitOfWork unitOfWork, IRepository<Users.Entities.User, long> repository) : base(unitOfWork,
            repository) {
            _userRepository = Repository<IUserRepository>();
            _userMapRepository = Repository<IUserMapRepository>();
        }

        /// <summary>
        ///     从缓存中获取用户
        /// </summary>
        /// <param name="userId">会员Id</param>
        public Users.Entities.User GetSingle(long userId) {
            return ObjectCache.GetOrSet(() => { return Repository<IUserRepository>().GetSingle(userId); },
                $"{_singleUserCacheKey}_Id_{userId}").Value;
        }

        /// <summary>
        ///     会员s the team.
        /// </summary>
        /// <param name="userId">会员Id</param>
        public Users.Entities.User UserTeam(long userId) {
            return ObjectCache.GetOrSet(() => { return Repository<IUserRepository>().UserTeam(userId); },
                $"{_singleUserCacheKey}_UserTeam_{userId}").Value;
        }

        /// <summary>
        ///     获取用户
        /// </summary>
        /// <param name="userId">会员Id</param>
        public Users.Entities.User GetNomarlUser(long userId) {
            var user = GetSingle(userId);
            if (user != null && user.Status == Status.Normal) {
                return user;
            }

            return null;
        }

        public Users.Entities.User GetSingleByUserNameOrMobile(string userName) {
            var user = GetSingle(r => r.UserName == userName || r.Email == userName || r.Mobile == userName);
            if (user != null) {
                user = GetUserDetail(user.Id);
                return user;
            }
            return null;
        }

        /// <summary>
        ///     获取s the single.
        /// </summary>
        /// <param name="userName">Name of the 会员.</param>
        public Users.Entities.User GetSingle(string userName) {
            if (userName.IsNullOrEmpty()) {
                return null;
            }

            var cacheKey = _singleUserCacheKey + "_UserName_" + userName;
            if (!ObjectCache.TryGet(cacheKey, out Users.Entities.User result)) {
                result = Repository<IUserRepository>().GetSingle(userName.Trim());
                if (result != null) {
                    ObjectCache.Set(cacheKey, result);
                }
            }

            return result;
        }

        /// <summary>
        ///     获取平台用户
        /// </summary>
        public Users.Entities.User PlanformUser() {
            //平台用户的用户名固定为planform
            return GetSingle("admin");
        }

        /// <summary>
        ///     获取s the single by mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        public Users.Entities.User GetSingleByMobile(string mobile) {
            var result = Repository<IUserRepository>().GetSingleByMobile(mobile); //不能用缓存，手机号码可以修改
            return result;
        }

        /// <summary>
        ///     获取s the single by mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        public Users.Entities.User GetSingleByMail(string mail) {
            var result = Repository<IUserRepository>().GetSingleByMail(mail); //不能用缓存，邮箱可以修改
            return result;
        }

        /// <summary>
        ///     获取用户的详细信息，包括User表、UserDetail、UserMap表
        /// </summary>
        /// <param name="userId">会员Id</param>
        public Users.Entities.User GetUserDetail(long userId) {
            var cacheKey = _singleUserCacheKey + "_Detail_Id_" + userId;
            if (!ObjectCache.TryGet(cacheKey, out Users.Entities.User result)) {
                result = Repository<IUserRepository>().GetUserDetail(userId);
                if (result != null) {
                    ObjectCache.Set(cacheKey, result);
                    //  result.UserGradeConfig = Resolve<IGradeService>().GetGrade(result.GradeId);
                }
            }

            return result;
        }

        /// <summary>
        ///     获取s the 会员 detail.
        /// </summary>
        /// <param name="UserName">Name of the 会员.</param>
        public Users.Entities.User GetUserDetail(string UserName) {
            if (UserName.IsNullOrEmpty()) {
                return null;
            }

            var cacheKey = _singleUserCacheKey + "_Detail_UserName_" + UserName;
            if (!ObjectCache.TryGet(cacheKey, out Users.Entities.User result)) {
                result = Repository<IUserRepository>().GetUserDetail(UserName.Trim());
                if (result != null) {
                    ObjectCache.Set(cacheKey, result);
                }
            }

            return result;
        }

        /// <summary>
        ///     更新s the specified 视图.
        /// </summary>
        /// <param name="model">The 视图.</param>
        /// <!-- 对于成员“M:Alabo.Domains.Services.WriteService`2.Update(`0)”忽略有格式错误的 XML 注释 -->
        public bool UpdateUser(Users.Entities.User model) {
            var result = _userRepository.UpdateSingle(model);

            if (result) {
                DeleteUserCache(model.Id, model.UserName);
            }

            return result;
        }

        /// <summary>
        ///     Existses the name of the 会员.
        /// </summary>
        /// <param name="name">The name.</param>
        public bool ExistsUserName(string name) {
            return _userRepository.ExistsName(name);
        }

        /// <summary>
        ///     Existses the mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        public bool ExistsMail(string mail) {
            return _userRepository.ExistsMail(mail);
        }

        /// <summary>
        ///     Existses the mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        public bool ExistsMobile(string mobile) {
            return _userRepository.ExistsMobile(mobile);
        }

        /// <summary>
        ///     获取s the list.
        /// </summary>
        /// <param name="userIds">The 会员 ids.</param>
        public IList<Users.Entities.User> GetList(IList<long> userIds) {
            return _userRepository.GetList(userIds);
        }

        /// <summary>
        ///     获取s the 会员 detail by open identifier.
        /// </summary>
        /// <param name="openId">The open identifier.</param>
        public Users.Entities.User GetUserDetailByOpenId(string openId) {
            var find = Resolve<IUserDetailService>().GetSingle(r => r.OpenId == openId);
            if (find != null) {
                var user = GetUserDetail(find.UserId);
                return user;
            }

            return null;
        }

        /// <summary>
        ///     会员注册
        /// </summary>
        /// <param name="user">The 会员.</param>
        public Users.Entities.User AddUser(Users.Entities.User user) {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .Where(r => r.Status == Status.Normal).ToList();
            var result = _userRepository.Add(user, moneyTypes);
            // 会员注册成功以后操作
            // Resolve<IUserRegAfterService>().AddBackJob(result);

            // 调试用
            Resolve<IUserRegAfterService>().AfterUserRegTask(result.Id);
            return result;
        }

        /// <summary>
        ///     获取最大的ID
        /// </summary>
        public long MaxUserId() {
            var model = _userRepository.MaxUserId();
            return model;
        }

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="viewUser">The 视图 会员.</param>
        public PagedList<ViewUser> GetViewUserPageList(ViewUser viewUser) {
            var userInput = AutoMapping.SetValue<UserInput>(viewUser);
            return GetViewUserPageList(userInput);
        }

        /// <summary>
        ///     会员分页查询
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewUser> GetViewUserPageList(object query) {
            var dic = query.ToObject<Dictionary<string, string>>();
            var userInput = AutoMapping.SetValue<UserInput>(dic);
            return GetViewUserPageList(userInput);
        }

        /// <summary>
        ///     管理员查询会员分页
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewAdminUser> GetViewAdminUserPageList(object query) {
            var dic = query.ToObject<Dictionary<string, string>>();
            var userInput = AutoMapping.SetValue<UserInput>(dic);
            return GetViewUserPageList<ViewAdminUser>(userInput);
        }

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        public PagedList<ViewUser> GetViewUserPageList(UserInput userInput) {
            var viewUserList = _userRepository.GetViewUserList(userInput, out var count);
            var userIds = viewUserList.Select(r => r.Id).Distinct().ToList();

            var userDetailList =
                Resolve<IUserDetailService>().GetList(r => userIds.Contains(r.UserId)); //获取UserDetail表信息

            var parentIds = viewUserList.Select(r => r.ParentId).Distinct().ToList();
            var parentUsers = _userRepository.GetList(parentIds);
            //TODO 9月重构注释
            //var identityList = Resolve<IIdentityService>().GetList(r => userIds.Contains(r.UserId)).ToList();

            IList<ViewUser> userResult = new List<ViewUser>();

            foreach (var item in viewUserList) {
                var viewUser = new ViewUser {
                    Id = item.Id,
                    UserName = GetUserStyle(item),
                    Name = item.Name,
                    Email = item.Email,
                    Status = item.Status,
                    Mobile = item.Mobile,
                    GradeId = item.GradeId
                };
                var grade = Resolve<IGradeService>().GetGrade(item.GradeId);
                viewUser.UserGradeConfig = grade;

                if (grade != null) {
                    viewUser.GradeName = grade.Name;
                }

                var userDetail = userDetailList.FirstOrDefault(r => r.UserId == item.Id);
                if (userDetail == null) {
                    continue;
                }

                viewUser.CreateTime = userDetail.CreateTime;
                viewUser.Sex = userDetail.Sex;

                viewUser.UserGradeConfig = Resolve<IGradeService>().GetGrade(viewUser.GradeId);

                var parentItemUser = parentUsers.FirstOrDefault(r => r.Id == item.ParentId);
                if (parentItemUser != null) {
                    viewUser.ParentName = GetUserStyle(parentItemUser);
                    viewUser.ParentId = parentItemUser.Id;
                }
                //TODO 9月重构注释
                //var identityItem = identityList.FirstOrDefault(r => r.UserId == item.Id);
                //if (identityItem != null) {
                //    viewUser.IdentityStatus = identityItem.Status;
                //}

                if (userInput.FilterType == FilterType.User) {
                    viewUser.UserName = item.GetUserName();
                }
                userResult.Add(viewUser);
            }

            return PagedList<ViewUser>.Create(userResult, count, userInput.PageSize, userInput.PageIndex);
        }

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        public PagedList<T> GetViewUserPageList<T>(UserInput userInput) where T : class, IUserView, new() {
            var viewUserList = _userRepository.GetViewUserList(userInput, out var count);
            var userIds = viewUserList.Select(r => r.Id).Distinct().ToList();

            var userDetailList =
                Resolve<IUserDetailService>().GetList(r => userIds.Contains(r.UserId)); //获取UserDetail表信息

            var parentIds = viewUserList.Select(r => r.ParentId).Distinct().ToList();
            var parentUsers = _userRepository.GetList(parentIds);
            // var identityList = Resolve<IIdentityService>().GetList(r => userIds.Contains(r.UserId)).ToList();

            IList<T> userResult = new List<T>();

            foreach (var item in viewUserList) {
                var viewUser = new T() {
                    Id = item.Id,
                    UserName = GetUserStyle(item),
                    Name = item.Name,
                    Email = item.Email,
                    Status = item.Status,
                    Mobile = item.Mobile,
                    GradeId = item.GradeId
                };
                var grade = Resolve<IGradeService>().GetGrade(item.GradeId);
                viewUser.UserGradeConfig = grade;

                if (grade != null) {
                    viewUser.GradeName = grade.Name;
                }

                var userDetail = userDetailList.FirstOrDefault(r => r.UserId == item.Id);
                if (userDetail == null) {
                    continue;
                }

                viewUser.CreateTime = userDetail.CreateTime;
                viewUser.Sex = userDetail.Sex;

                viewUser.UserGradeConfig = Resolve<IGradeService>().GetGrade(viewUser.GradeId);

                var parentItemUser = parentUsers.FirstOrDefault(r => r.Id == item.ParentId);
                if (parentItemUser != null) {
                    viewUser.ParentName = GetUserStyle(parentItemUser);
                    viewUser.ParentId = parentItemUser.Id;
                }

                //var identityItem = identityList.FirstOrDefault(r => r.UserId == item.Id);
                //if (identityItem != null) {
                //    viewUser.IdentityStatus = identityItem.Status;
                //}

                if (userInput.FilterType == FilterType.User) {
                    viewUser.UserName = item.GetUserName();
                }

                userResult.Add(viewUser);
            }

            return PagedList<T>.Create(userResult, count, userInput.PageSize, userInput.PageIndex);
        }

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="userId">会员Id</param>
        public void DeleteUserCache(long userId) {
            var user = GetSingle(userId);
            if (user != null) {
                DeleteUserCache(user.Id, user.UserName);
            }
        }

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="UserName">Name of the 会员.</param>
        public void DeleteUserCache(long userId, string UserName) {
            var cacheKey = _singleUserCacheKey + "_Id_" + userId;
            ObjectCache.Remove(cacheKey);
            cacheKey = _singleUserCacheKey + "_UserName_" + UserName.Trim();
            ObjectCache.Remove(cacheKey);
            cacheKey = _singleUserCacheKey + "_Detail_UserName_" + UserName.Trim();
            ObjectCache.Remove(cacheKey);
            cacheKey = _singleUserCacheKey + "_Detail_Id_" + userId;
            ObjectCache.Remove(cacheKey);
            cacheKey = "UserAllGradeId" + userId;
            ObjectCache.Remove(cacheKey);
            cacheKey = "dynamic_GetSingleUser" + UserName;
            ObjectCache.Remove(cacheKey);
        }

        /// <summary>
        ///     用户美化
        /// </summary>
        /// <param name="user">用户</param>
        public string GetUserStyle(Users.Entities.User user) {
            if (user == null) {
                return string.Empty;
            }

            var gradeConfig = Resolve<IGradeService>().GetGrade(user.GradeId);
            var userName =
                $" <img src='{Resolve<IApiService>().ApiImageUrl(gradeConfig.Icon)}' alt='{gradeConfig.Name}' class='user-pic' style='width:18px;height:18px;' /><a class='primary-link margin-8' " +
                //$"href='/Admin/User/Edit?id={user.Id}' " +
                $"title='{user.UserName}({user.Name}) 等级:{gradeConfig?.Name}'>{user.UserName}({user.Name})</a>";

            return userName;
        }

        /// <summary>
        ///     会员中心用户美化
        /// </summary>
        /// <param name="user">The user.</param>
        public string GetHomeUserStyle(Users.Entities.User user) {
            if (user == null) {
                return string.Empty;
            }

            var gradeConfig = Resolve<IGradeService>().GetGrade(user.GradeId);
            var userName =
                $" <img src='{gradeConfig.Icon}' alt='{gradeConfig.Name}' class='user-pic' style='width:18px;height:18px;' /><a class='primary-link margin-8'" +
                $" href='/Home/User/Edit?id=" + $"{user.Id}'" +
                $" title='{user.UserName}({user.Name}) 等级:{gradeConfig?.Name}'>{user.UserName}({user.Name})</a>";

            return userName;
        }

        /// <summary>
        ///     数据同步 获取等级团队中心
        /// </summary>
        public long GetTeamCenterService(long userId, long grade) {
            var grades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var user = GetSingle(userId);

            if (user.ParentId == 0 || user.Id == user.ParentId) {
                return 0;
            }

            return GetTeamCenterService(user.ParentId, grade);
        }

        /// <inheritdoc />
        /// <summary>
        ///     判断当前登陆用户是否为后台管理
        /// </summary>
        /// <param name="userId">用户Id</param>
        public bool IsAdmin(long userId) {
            var find = Resolve<IEmployeeService>().GetSingle(r => r.UserId == userId);
            if (find == null) {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     我推荐的会员
        /// </summary>
        public PagedList<ViewHomeUser> GetRecommondUserPage(object query) {
            var user = query.ToUserObject();
            var users = Resolve<IUserService>().GetPagedList<ViewHomeUser>(query, u => u.ParentId == user.Id);
            users.ForEach(u => {
                var userStyle = AutoMapping.SetValue<Users.Entities.User>(u);
                u.UserName = GetHomeUserStyle(userStyle);
                u.GradeName = Resolve<IGradeService>().GetGrade(u.GradeId)?.Name;
            });
            return users;
        }

        /// <summary>
        /// 获取用户Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetUserToken(Users.Entities.User user) {
            return ObjectCache.GetOrSet(() => {
                if (user == null) {
                    throw new ValidException("用户不能为空");
                }

                //     var accout = Resolve<IAccountService>().GetSingle(r => r.UserId == user.Id);
                //if (accout == null) {
                //    throw new ValidException("用户不合法");
                //}

                var key = user?.Id + user?.UserName + HttpWeb.Site.Id + HttpWeb.Tenant;
                var key2 = user?.Id + user?.UserName + HttpWeb.Site.Id;
                var key3 = user?.Id + user?.UserName + HttpWeb.Site.Id +
                           HttpWeb.Site.Id + user?.Detail?.Id;
                var token = key.ToMd5HashString() +
                            key2.ToMd5HashString().Substring(3, 18) + key3.ToMd5HashString();
                return token;
            }, "GetUserToken_" + user?.Id).Value;
        }
    }
}