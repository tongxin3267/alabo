using Alabo.Data.People.ShareHolders.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.ShareHolders.Domain.Services {

    public class ShareHolderService : ServiceBase<ShareHolder, ObjectId>, IShareHolderService {

        public ShareHolderService(IUnitOfWork unitOfWork, IRepository<ShareHolder, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}