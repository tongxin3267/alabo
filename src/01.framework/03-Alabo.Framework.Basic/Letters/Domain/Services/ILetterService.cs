using Alabo.Domains.Services;
using Alabo.Framework.Basic.Letters.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Letters.Domain.Services {

    public interface ILetterService : IService<Letter, ObjectId> {
    }
}