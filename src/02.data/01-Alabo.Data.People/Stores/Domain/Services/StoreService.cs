using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Services {

    public class StoreService : ServiceBase<Entities.Store, ObjectId>, IStoreService {

        public StoreService(IUnitOfWork unitOfWork, IRepository<Entities.Store, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}