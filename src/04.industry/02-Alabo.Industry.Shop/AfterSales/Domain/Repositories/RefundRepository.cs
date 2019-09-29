using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.AfterSales.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.AfterSales.Domain.Repositories
{
    public class RefundRepository : RepositoryMongo<Refund, ObjectId>, IRefundRepository
    {
        public RefundRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}