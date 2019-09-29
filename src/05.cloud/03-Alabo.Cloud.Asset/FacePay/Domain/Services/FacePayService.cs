using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.Asset.FacePay.Domain.Services
{
    public class FacePayService : ServiceBase<Entities.FacePay, ObjectId>, IFacePayService
    {
        public FacePayService(IUnitOfWork unitOfWork, IRepository<Entities.FacePay, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}