using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public interface ISpecialService : IService<Special, ObjectId>
    {
        ServiceResult AddOrUpdate(Special model);
    }
}