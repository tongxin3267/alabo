using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Services
{
    public class AfterSaleService : ServiceBase<AfterSale, ObjectId>, IAfterSaleService
    {
        public AfterSaleService(IUnitOfWork unitOfWork, IRepository<AfterSale, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}