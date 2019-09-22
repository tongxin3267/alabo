using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public class ShareOrderReportRepository : RepositoryMongo<ShareOrderReport, ObjectId>, IShareOrderReportRepository {

        public ShareOrderReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}