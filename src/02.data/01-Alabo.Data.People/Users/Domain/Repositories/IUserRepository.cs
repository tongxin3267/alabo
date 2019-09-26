using System.Collections.Generic;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users.Domain.Repositories {

    public interface IUserRepository : IRepository<User, long> {

        Alabo.Users.Entities.User UserTeam(long userId);

        Alabo.Users.Entities.User GetSingle(long userId);

        Alabo.Users.Entities.User GetSingle(string UserName);

        Alabo.Users.Entities.User GetSingleByMail(string mail);

        Alabo.Users.Entities.User GetSingleByMobile(string mobile);

        Alabo.Users.Entities.User GetUserDetail(long userId);

        Alabo.Users.Entities.User GetUserDetail(string UserName);

        Alabo.Users.Entities.User Add(Alabo.Users.Entities.User User, List<MoneyTypeConfig> moneyTypes);

        bool UpdateSingle(Alabo.Users.Entities.User user);

        bool CheckUserExists(string UserName, string password, out long userId);

        IList<Alabo.Users.Entities.User> GetList(IList<long> userIds);

        bool ExistsName(string name);

        long MaxUserId();

        bool ExistsMail(string mail);

        bool ExistsMobile(string mobile);

        /// <summary>
        /// 物理删除会员
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Delete(long userId);

        IList<Alabo.Users.Entities.User> GetViewUserList(UserInput userInput, out long count);

        /// <summary>
        ///     会员删除时推荐关系修改
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="parentId"></param>
        bool UpdateRecommend(List<long> userIds, long parentId);
    }
}