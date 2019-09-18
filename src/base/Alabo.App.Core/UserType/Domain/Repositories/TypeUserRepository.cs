using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.UserType.Domain.Repositories {

    public class TypeUserRepository : RepositoryEfCore<TypeUser, long>, ITypeUserRepository {

        public TypeUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}