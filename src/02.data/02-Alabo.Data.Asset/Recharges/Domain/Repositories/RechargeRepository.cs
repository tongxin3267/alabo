using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Recharges.Domain.Repositories
{
    public class RechargeRepository : RepositoryMongo<Recharge, long>, IRechargeRepository
    {
        public RechargeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}