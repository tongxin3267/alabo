using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public class TradeRepository : RepositoryEfCore<Trade, long>, ITradeRepository {

        public TradeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}