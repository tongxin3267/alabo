using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Users.Entities;
using System.Collections.Generic;

namespace Alabo.Data.People.Users.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
        User UserTeam(long userId);

        User GetSingle(long userId);

        User GetSingle(string userName);

        User GetSingleByMail(string mail);

        User GetSingleByMobile(string mobile);

        User GetUserDetail(long userId);

        User GetUserDetail(string userName);

        User Add(User user, List<MoneyTypeConfig> moneyTypes);

        bool UpdateSingle(User user);

        bool CheckUserExists(string userName, string password, out long userId);

        IList<User> GetList(IList<long> userIds);

        bool ExistsName(string name);

        long MaxUserId();

        bool ExistsMail(string mail);

        bool ExistsMobile(string mobile);

        /// <summary>
        ///     物理删除会员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Delete(long userId);

        IList<User> GetViewUserList(UserInput userInput, out long count);

        /// <summary>
        ///     会员删除时推荐关系修改
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="parentId"></param>
        bool UpdateRecommend(List<long> userIds, long parentId);
    }
}