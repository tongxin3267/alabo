using Alabo.Cloud.Wikis.Settings.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Wikis.Settings.Domain.Repositories
{
    public interface IWikiClassRepository : IRepository<WikiClass, ObjectId>
    {
    }
}