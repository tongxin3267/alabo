using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public class AutoConfigRepository : RepositoryEfCore<AutoConfig, long>, IAutoConfigRepository {

        public AutoConfigRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}