using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Refunds.Domain.Repositories
{
    public interface IRefundRepository : IRepository<Refund, long>
    {
    }
}