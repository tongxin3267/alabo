using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Refunds.Domain.Services
{
    public class RefundService : ServiceBase<Refund, long>, IRefundService
    {
        public RefundService(IUnitOfWork unitOfWork, IRepository<Refund, long> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}