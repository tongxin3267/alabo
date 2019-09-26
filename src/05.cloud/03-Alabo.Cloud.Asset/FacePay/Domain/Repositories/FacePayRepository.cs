using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Asset.FacePay.Domain.Repositories
{
    public class FacePayRepository : RepositoryMongo<Entities.FacePay, ObjectId>, IFacePayRepository
    {
        public FacePayRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}