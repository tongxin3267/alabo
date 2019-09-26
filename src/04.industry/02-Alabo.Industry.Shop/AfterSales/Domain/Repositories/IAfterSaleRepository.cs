using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories {

    public interface IAfterSaleRepository : IRepository<Entities.AfterSale, ObjectId> {
    }
}