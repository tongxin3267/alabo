using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Refunds.Domain.Repositories
{
    public class RefundRepository : RepositoryEfCore<Refund, long>, IRefundRepository
    {
        public RefundRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}