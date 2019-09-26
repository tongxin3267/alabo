using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Settlements.Domain.Repositories
{
    public class SettlementRepository : RepositoryMongo<Settlement, long>, ISettlementRepository
    {
        public SettlementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}