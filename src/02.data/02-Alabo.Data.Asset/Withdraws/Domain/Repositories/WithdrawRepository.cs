using Alabo.App.Asset.Withdraws.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Withdraws.Domain.Repositories
{
    public class WithdrawRepository : RepositoryEfCore<Withdraw, long>, IWithdrawRepository
    {
        public WithdrawRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}