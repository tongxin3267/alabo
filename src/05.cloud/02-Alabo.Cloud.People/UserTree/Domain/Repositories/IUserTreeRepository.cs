using System.Collections.Generic;
using Alabo.Cloud.People.UserTree.Domain.Configs;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Users.Entities;

namespace Alabo.Cloud.People.UserTree.Domain.Repositories {

    public interface IUserTreeRepository : IRepository<UserMap, long> {

        List<Users.Entities.UserTree> GetTree(long userId, UserTreeConfig userTreeConfig, UserTypeConfig userType,
            List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig);
    }
}