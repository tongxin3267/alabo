using MongoDB.Bson;
using Alabo.App.Core.Markets.Visitors.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Markets.Visitors.Domain.Services {

    public class VisitorService : ServiceBase<Visitor, ObjectId>, IVisitorService {

        public VisitorService(IUnitOfWork unitOfWork, IRepository<Visitor, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}