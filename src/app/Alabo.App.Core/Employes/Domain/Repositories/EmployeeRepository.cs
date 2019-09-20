using MongoDB.Bson;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Employes.Domain.Repositories {

    public class EmployeeRepository : RepositoryMongo<Employee, ObjectId>, IEmployeeRepository {

        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}