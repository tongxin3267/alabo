using MongoDB.Bson;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.UserType.Domain.Repositories {

    public interface ITypeGradeRepository : IRepository<TypeGrade, ObjectId> {
    }
}