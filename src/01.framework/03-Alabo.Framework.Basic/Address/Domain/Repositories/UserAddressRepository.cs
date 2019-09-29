using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Regions.Domain.Repositories;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Repositories
{
    public class UserAddressRepository : RepositoryMongo<UserAddress, ObjectId>, IUserAddressRepository
    {
        public UserAddressRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}