using Alabo.Data.People.ShareHolders.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.ShareHolders.Domain.Services
{
    public interface IShareHolderService : IService<ShareHolder, ObjectId>
    {
    }
}