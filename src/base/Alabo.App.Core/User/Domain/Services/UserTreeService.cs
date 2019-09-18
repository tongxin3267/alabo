using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserTreeService : ServiceBase, IUserTreeService {

        public UserTreeService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public List<UserTree> GetUserTree(long UserId) {
            var userTreeConfig = Resolve<IAutoConfigService>().GetValue<UserTreeConfig>();
            var userTypeConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            var userGradeConfigList = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userServiceConfig = Resolve<IAutoConfigService>().GetList<UserTypeConfig>().FirstOrDefault();
            return Repository<IUserMapRepository>().GetTree(UserId, userTreeConfig, userTypeConfig, userGradeConfigList,
                userServiceConfig);
        }
    }
}