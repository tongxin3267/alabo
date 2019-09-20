using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Attach.Domain.Services {

    public class ExtendService : ServiceBase<Extend, ObjectId>, IExtendService {

        public ExtendService(IUnitOfWork unitOfWork, IRepository<Extend, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}