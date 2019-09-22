using MongoDB.Bson;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Employes.Domain.Repositories {

    public interface IEmployeeRepository : IRepository<Employee, ObjectId> {
    }
}