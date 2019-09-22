using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IUserTreeRepository : IRepository<UserMap, long> {

        List<UserTree> GetTree(long userId, UserTreeConfig userTreeConfig, UserTypeConfig userType,
            List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig);
    }
}