using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class UserAddressRepository : RepositoryMongo<UserAddress, ObjectId>, IUserAddressRepository {

        public UserAddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}