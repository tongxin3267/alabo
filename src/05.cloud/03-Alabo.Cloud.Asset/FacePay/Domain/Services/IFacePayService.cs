using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Asset.FacePay.Domain.Services {

    public interface IFacePayService : IService<Entities.FacePay, ObjectId> {
    }
}