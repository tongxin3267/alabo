using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Settlements.Domain.Services
{
    public interface ISettlementService : IService<Settlement, long>
    {
    }
}