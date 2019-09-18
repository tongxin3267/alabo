using MongoDB.Bson;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public interface ITypeGradeService : IService<TypeGrade, ObjectId> {
    }
}