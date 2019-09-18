using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Attach.Domain.Services {

    public interface ICommentService : IService<Comment, ObjectId> {
    }
}