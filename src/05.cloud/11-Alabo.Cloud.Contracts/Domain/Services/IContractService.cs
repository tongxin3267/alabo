using Alabo.Cloud.Contracts.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Contracts.Domain.Services
{
    public interface IContractService : IService<Contract, ObjectId>
    {
    }
}