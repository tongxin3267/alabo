using Alabo.Cloud.Shop.Comments.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Comments.Domain.Services
{
    public class CommentService : ServiceBase<Comment, ObjectId>, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork, IRepository<Comment, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}