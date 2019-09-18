using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Attach.Domain.Repositories {

    public class CommentRepository : RepositoryMongo<Comment, ObjectId>, ICommentRepository {

        public CommentRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}