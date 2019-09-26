using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services {

    public class AfterSaleService : ServiceBase<Entities.AfterSale, ObjectId>, IAfterSaleService {

        public AfterSaleService(IUnitOfWork unitOfWork, IRepository<Entities.AfterSale, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}