using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Refunds.Domain.Repositories
{
    public class RefundRepository : RepositoryMongo<Refund, long>, IRefundRepository
    {
        public RefundRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}