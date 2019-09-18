using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Attach.Domain.Services {

    public class LetterService : ServiceBase<Letter, ObjectId>, ILetterService {

        public LetterService(IUnitOfWork unitOfWork, IRepository<Letter, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}