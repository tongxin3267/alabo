using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public interface ITradeRepository : IRepository<Trade, long> {
    }
}