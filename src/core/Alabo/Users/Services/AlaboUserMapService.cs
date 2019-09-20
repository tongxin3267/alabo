using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Users.Domain.Services;
using System.Collections.Generic;

namespace Alabo.App.Core.User.Domain.Services {

    public class AlaboUserMapService : ServiceBase<UserMap, long>, IAlaboUserMapService {
        private readonly IDictionary<long, UserMap> _parentMapCache = new Dictionary<long, UserMap>();

        public AlaboUserMapService(IUnitOfWork unitOfWork, IRepository<UserMap, long> repository) : base(unitOfWork,
            repository) {
        }
    }
}