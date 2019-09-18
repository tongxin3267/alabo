using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Attach.Domain.Services {

    public interface ILetterService : IService<Letter, ObjectId> {
    }
}