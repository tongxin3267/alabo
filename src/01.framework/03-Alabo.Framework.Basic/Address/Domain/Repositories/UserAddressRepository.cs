using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class UserAddressRepository : RepositoryMongo<UserAddress, ObjectId>, IUserAddressRepository {

        public UserAddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}