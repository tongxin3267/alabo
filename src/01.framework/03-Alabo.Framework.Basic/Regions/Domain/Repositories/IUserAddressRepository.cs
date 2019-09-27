using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Regions.Domain.Repositories
{
    public interface IUserAddressRepository : IRepository<UserAddress, ObjectId>
    {
    }
}