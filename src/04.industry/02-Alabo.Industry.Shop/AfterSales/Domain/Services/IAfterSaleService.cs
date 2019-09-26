using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services {

    public interface IAfterSaleService : IService<Entities.AfterSale, ObjectId> {
    }
}