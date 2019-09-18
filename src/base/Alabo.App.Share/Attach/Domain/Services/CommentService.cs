using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Attach.Domain.Services {

    public class CommentService : ServiceBase<Comment, ObjectId>, ICommentService {

        public CommentService(IUnitOfWork unitOfWork, IRepository<Comment, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}