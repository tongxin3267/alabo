using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Domain.Repositories
{
    public interface IParentInternalRepository : IRepository<ParentInternal, ObjectId>
    {
    }
}