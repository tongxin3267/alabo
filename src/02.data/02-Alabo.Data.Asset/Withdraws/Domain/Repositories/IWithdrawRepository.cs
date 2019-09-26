using Alabo.App.Asset.Withdraws.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Withdraws.Domain.Repositories
{
    public interface IWithdrawRepository : IRepository<Withdraw, long>
    {
    }
}