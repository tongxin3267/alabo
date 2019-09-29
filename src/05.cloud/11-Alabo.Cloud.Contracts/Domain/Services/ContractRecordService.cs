using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Services
{
    public class ContractRecordService : ServiceBase<ContractRecord, ObjectId>, IContractRecordService
    {
        public ContractRecordService(IUnitOfWork unitOfWork, IRepository<ContractRecord, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}