using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Users.Entities;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserTreeService : ServiceBase, IUserTreeService {

        public UserTreeService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public List<UserTree> GetTree(long userId, UserTreeConfig userTreeConfig, UserTypeConfig userType, List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig) {
            //TODO 2019年9月22日 重构组织结构图，查看之前代码
            throw new System.NotImplementedException();
        }

        public List<UserTree> GetUserTree(long UserId) {
            var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            var userTypeConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            var userGradeConfigList = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userServiceConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            return Repository<IUserTreeRepository>().GetTree(UserId, userTreeConfig, userTypeConfig, userGradeConfigList,
                userServiceConfig);
        }
    }
}