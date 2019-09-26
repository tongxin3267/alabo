using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Settlements.Domain.Services
{
    public class SettlementService : ServiceBase<Settlement, long>, ISettlementService
    {
        public SettlementService(IUnitOfWork unitOfWork, IRepository<Settlement, long> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}