using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;

namespace Alabo.App.Core.User.Domain.Repositories {

    public interface IUserAddressRepository : IRepository<UserAddress, ObjectId> {
    }
}