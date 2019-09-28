using Alabo.Data.People.Users.Dtos;
using Alabo.Data.People.Users.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Users.Entities;
using System.Collections.Generic;

namespace Alabo.Data.People.Users.Domain.Services
{
    /// <summary>
    ///     用户操作相关方法
    /// </summary>
    public interface IUserService : IService<User, long>
    {
        /// <summary>
        ///     会员s the team.
        /// </summary>
        /// <param name="userId">会员Id</param>
        User UserTeam(long userId);

        /// <summary>
        ///     从缓存当中读取用户
        /// </summary>
        /// <param name="userId">会员Id</param>
        User GetSingle(long userId);

        /// <summary>
        ///     获取s the single.
        /// </summary>
        /// <param name="userName">Name of the 会员.</param>
        User GetSingle(string userName);

        /// <summary>
        ///     根据用户名或手机号码获取用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User GetSingleByUserNameOrMobile(string userName);

        /// <summary>
        ///     获取s the single by mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        User GetSingleByMail(string mail);

        /// <summary>
        ///     获取s the single by mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        User GetSingleByMobile(string mobile);

        /// <summary>
        ///     获取用户
        /// </summary>
        /// <param name="userId">会员Id</param>
        User GetNomarlUser(long userId);

        /// <summary>
        ///     获取平台用户,没有平台用户，很多功能没有办法使用
        /// </summary>
        User PlatformUser();

        /// <summary>
        ///     获取用户的详细信息，包括User表、UserDetail、UserMap表
        /// </summary>
        /// <param name="userId">会员Id</param>
        User GetUserDetail(long userId);

        /// <summary>
        ///     获取s the 会员 detail by open identifier.
        /// </summary>
        /// <param name="openId">The open identifier.</param>
        User GetUserDetailByOpenId(string openId);

        /// <summary>
        ///     获取s the 会员 detail.
        /// </summary>
        /// <param name="userName">Name of the 会员.</param>
        User GetUserDetail(string userName);

        /// <summary>
        ///     获取s the list.
        /// </summary>
        /// <param name="userIds">The 会员 ids.</param>
        IList<User> GetList(IList<long> userIds);

        /// <summary>
        ///     更新s the specified 会员.
        /// </summary>
        /// <param name="user">The 会员.</param>
        bool UpdateUser(User user);

        /// <summary>
        ///     Existses the name of the 会员.
        /// </summary>
        /// <param name="userName">Name of the 会员.</param>
        bool ExistsUserName(string userName);

        /// <summary>
        ///     Existses the mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        bool ExistsMail(string mail);

        /// <summary>
        ///     Existses the mobile.
        /// </summary>
        /// <param name="mobile">The mobile.</param>
        bool ExistsMobile(string mobile);

        /// <summary>
        ///     添加s the specified 会员.
        /// </summary>
        /// <param name="user">The 会员.</param>
        User AddUser(User user);

        /// <summary>
        ///     会员分页查询
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewUser> GetViewUserPageList(object query);

        /// <summary>
        ///     会员分页查询
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewAdminUser> GetViewAdminUserPageList(object query);

        PagedList<T> GetViewUserPageList<T>(UserInput userInput) where T : class, IUserView, new();

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        PagedList<ViewUser> GetViewUserPageList(UserInput userInput);

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        PagedList<ViewUser> GetViewUserPageList(ViewUser userInput);

        /// <summary>
        ///     获取最大的ID
        /// </summary>
        long MaxUserId();

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="userName">Name of the 会员.</param>
        void DeleteUserCache(long userId, string userName);

        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="userId">会员Id</param>
        void DeleteUserCache(long userId);

        /// <summary>
        ///     用户美化
        /// </summary>
        /// <param name="user">用户</param>
        /// ///
        string GetUserStyle(User user);

        /// <summary>
        ///     数据同步 获取等级城市中心
        /// </summary>
        long GetTeamCenterService(long userId, long grade);

        /// <summary>
        ///     判断当前登陆用户是否为后台管理
        /// </summary>
        /// <param name="userId">用户Id</param>
        bool IsAdmin(long userId);

        /// <summary>
        ///     Gets the home user style.
        /// </summary>
        /// <param name="user">The user.</param>
        string GetHomeUserStyle(User user);

        PagedList<ViewHomeUser> GetRecommondUserPage(object query);

        /// <summary>
        ///     根据用户获取Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GetUserToken(User user);
    }
}