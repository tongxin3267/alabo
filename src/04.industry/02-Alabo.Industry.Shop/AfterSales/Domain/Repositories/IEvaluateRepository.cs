using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories
{
    public interface IEvaluateRepository : IRepository<Evaluate, ObjectId>
    {
    }
}