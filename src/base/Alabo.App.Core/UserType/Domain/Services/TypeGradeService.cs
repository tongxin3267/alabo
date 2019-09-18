using MongoDB.Bson;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public class TypeGradeService : ServiceBase<TypeGrade, ObjectId>, ITypeGradeService {

        public TypeGradeService(IUnitOfWork unitOfWork, IRepository<TypeGrade, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}