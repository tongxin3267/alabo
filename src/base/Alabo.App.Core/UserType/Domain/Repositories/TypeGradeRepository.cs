using MongoDB.Bson;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.UserType.Domain.Repositories {

    public class TypeGradeRepository : RepositoryMongo<TypeGrade, ObjectId>, ITypeGradeRepository {

        public TypeGradeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}