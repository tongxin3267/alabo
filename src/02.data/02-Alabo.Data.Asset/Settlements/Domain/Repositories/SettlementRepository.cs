using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Settlements.Domain.Repositories
{
    public class SettlementRepository : RepositoryEfCore<Settlement, long>, ISettlementRepository
    {
        public SettlementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}