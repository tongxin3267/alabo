using Alabo.Cloud.Shop.Comments.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Comments.Domain.Repositories
{
    public interface ICommentRepository : IRepository<Comment, ObjectId>
    {
    }
}