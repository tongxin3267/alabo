using Alabo.Domains.Services;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services
{
    public interface IEvaluateService : IService<Evaluate, ObjectId>
    {
    }
}