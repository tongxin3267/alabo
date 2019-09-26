using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Settlements.Domain.Repositories
{
    public interface ISettlementRepository : IRepository<Settlement, long>
    {
    }
}