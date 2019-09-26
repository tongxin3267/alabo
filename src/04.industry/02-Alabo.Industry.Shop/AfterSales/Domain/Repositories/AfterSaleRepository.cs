using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories {

    public class AfterSaleRepository : RepositoryMongo<Entities.AfterSale, ObjectId>, IAfterSaleRepository {

        public AfterSaleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}