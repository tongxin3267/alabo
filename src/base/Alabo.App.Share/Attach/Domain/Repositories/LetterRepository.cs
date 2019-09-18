using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Attach.Domain.Repositories {

    public class LetterRepository : RepositoryMongo<Letter, ObjectId>, ILetterRepository {

        public LetterRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}