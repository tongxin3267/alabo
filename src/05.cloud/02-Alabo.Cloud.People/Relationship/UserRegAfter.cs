using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Market.Relationship.Domain.Services;
using Alabo.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Users.Entities;

namespace Alabo.App.Market.Relationship {

    /// <summary>
    ///     更新会员关系网
    /// </summary>
    public class UserRegAfter : IUserRegAfter {
        public long SortOrder { get; set; } = 1;

        public void Excecute(User user) {
            Ioc.Resolve<IRelationshipIndexService>().UserRegAfter(user);
            ;
        }
    }
}