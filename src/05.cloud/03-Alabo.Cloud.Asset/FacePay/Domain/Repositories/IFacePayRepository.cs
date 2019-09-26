using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Asset.FacePay.Domain.Repositories {

    public interface IFacePayRepository : IRepository<Entities.FacePay, ObjectId> {
    }
}