using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Repositories
{
    public class ContractRepository : RepositoryMongo<Contract, ObjectId>, IContractRepository
    {
        public ContractRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}