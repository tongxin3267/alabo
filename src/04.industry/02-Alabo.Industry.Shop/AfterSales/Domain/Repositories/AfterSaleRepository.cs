using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories
{
    public class AfterSaleRepository : RepositoryMongo<AfterSale, ObjectId>, IAfterSaleRepository
    {
        public AfterSaleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}