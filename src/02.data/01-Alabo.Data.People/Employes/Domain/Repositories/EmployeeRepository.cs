using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Employes.Domain.Repositories {

    public class EmployeeRepository : RepositoryMongo<Employee, ObjectId>, IEmployeeRepository {

        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}