using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IUserRepository : IRepository<Users.Entities.User, long> {

        Users.Entities.User UserTeam(long userId);

        Users.Entities.User GetSingle(long userId);

        Users.Entities.User GetSingle(string UserName);

        Users.Entities.User GetSingleByMail(string mail);

        Users.Entities.User GetSingleByMobile(string mobile);

        Users.Entities.User GetUserDetail(long userId);

        Users.Entities.User GetUserDetail(string UserName);

        Users.Entities.User Add(Users.Entities.User User, List<MoneyTypeConfig> moneyTypes);

        bool UpdateSingle(Users.Entities.User user);

        bool CheckUserExists(string UserName, string password, out long userId);

        IList<Users.Entities.User> GetList(IList<long> userIds);

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

        IList<Users.Entities.User> GetViewUserList(UserInput userInput, out long count);

        /// <summary>
        ///     会员删除时推荐关系修改
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="parentId"></param>
        bool UpdateRecommend(List<long> userIds, long parentId);
    }
}