using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IUserRepository : IRepository<Entities.User, long> {

        Entities.User UserTeam(long userId);

        Entities.User GetSingle(long userId);

        Entities.User GetSingle(string UserName);

        Entities.User GetSingleByMail(string mail);

        Entities.User GetSingleByMobile(string mobile);

        Entities.User GetUserDetail(long userId);

        Entities.User GetUserDetail(string UserName);

        Entities.User Add(Entities.User User, List<MoneyTypeConfig> moneyTypes);

        bool UpdateSingle(Entities.User user);

        bool CheckUserExists(string UserName, string password, out long userId);

        IList<Entities.User> GetList(IList<long> userIds);

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

        IList<Entities.User> GetViewUserList(UserInput userInput, out long count);

        /// <summary>
        ///     会员删除时推荐关系修改
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="parentId"></param>
        bool UpdateRecommend(List<long> userIds, long parentId);
    }
}