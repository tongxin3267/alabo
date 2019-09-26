using Alabo.Cloud.Shop.Comments.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Comments.Domain.Services
{
    public interface ICommentService : IService<Comment, ObjectId>
    {
    }
}