using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Repositories
{
    public class ContractRecordRepository : RepositoryMongo<ContractRecord, ObjectId>, IContractRecordRepository
    {
        public ContractRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}