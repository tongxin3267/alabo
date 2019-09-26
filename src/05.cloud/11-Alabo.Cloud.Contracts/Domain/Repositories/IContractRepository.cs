using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Repositories
{
    public interface IContractRepository : IRepository<Contract, ObjectId>
    {
    }
}