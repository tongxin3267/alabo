using Alabo.Cloud.Shop.Comments.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Comments.Domain.Repositories {

    public class CommentRepository : RepositoryMongo<Comment, ObjectId>, ICommentRepository {

        public CommentRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}