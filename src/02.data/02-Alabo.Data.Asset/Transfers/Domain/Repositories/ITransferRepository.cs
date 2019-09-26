using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Transfers.Domain.Repositories
{
    public interface ITransferRepository : IRepository<Transfer, long>
    {
    }
}