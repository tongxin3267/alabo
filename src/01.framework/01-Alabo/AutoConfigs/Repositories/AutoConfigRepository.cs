using Alabo.AutoConfigs.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.AutoConfigs.Repositories
{
    public class AutoConfigRepository : RepositoryEfCore<AutoConfig, long>, IAutoConfigRepository
    {
        public AutoConfigRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}