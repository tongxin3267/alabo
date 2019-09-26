using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Asset.Transfers.Domain.Repositories
{
    public class TransferRepository : RepositoryMongo<Transfer, long>, ITransferRepository
    {
        public TransferRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}