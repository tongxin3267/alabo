using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Recharges.Domain.Repositories
{
    public interface IRechargeRepository : IRepository<Recharge, long>
    {
    }
}