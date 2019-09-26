using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Services
{
    public class ContractService : ServiceBase<Contract, ObjectId>, IContractService
    {
        public ContractService(IUnitOfWork unitOfWork, IRepository<Contract, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}