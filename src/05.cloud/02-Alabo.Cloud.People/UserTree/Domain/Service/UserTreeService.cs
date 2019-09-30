using Alabo.Cloud.People.UserTree.Domain.Configs;
using Alabo.Cloud.People.UserTree.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Cloud.People.UserTree.Domain.Service
{
    public class UserTreeService : ServiceBase, IUserTreeService
    {
        public UserTreeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Users.Entities.UserTree> GetTree(long userId, UserTreeConfig userTreeConfig,
            UserTypeConfig userType, List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig)
        {
            //TODO 2019年9月22日 重构组织结构图，查看之前代码
            throw new NotImplementedException();
        }

        public List<Users.Entities.UserTree> GetUserTree(long UserId)
        {
            var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            var userTypeConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            var userGradeConfigList = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userServiceConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            return Repository<IUserTreeRepository>().GetTree(UserId, userTreeConfig, userTypeConfig,
                userGradeConfigList,
                userServiceConfig);
        }
    }
}