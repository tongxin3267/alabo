using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Asset.Refunds.Domain.Services
{
    public interface IRefundService : IService<Refund, long>
    {
    }
}