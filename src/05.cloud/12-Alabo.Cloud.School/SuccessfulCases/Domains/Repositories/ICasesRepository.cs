using Alabo.Cloud.School.SuccessfulCases.Domains.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SuccessfulCases.Domains.Repositories
{
    public interface ICasesRepository : IRepository<Cases, ObjectId>
    {
    }
}