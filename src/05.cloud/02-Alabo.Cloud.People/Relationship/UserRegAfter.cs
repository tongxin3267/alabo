using Alabo.Cloud.People.Relationship.Domain.Services;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Users.Entities;

namespace Alabo.Cloud.People.Relationship
{
    /// <summary>
    ///     更新会员关系网
    /// </summary>
    public class UserRegAfter : IUserRegAfter
    {
        public long SortOrder { get; set; } = 1;

        public void Excecute(User user)
        {
            Ioc.Resolve<IRelationshipIndexService>().UserRegAfter(user);
            ;
        }
    }
}