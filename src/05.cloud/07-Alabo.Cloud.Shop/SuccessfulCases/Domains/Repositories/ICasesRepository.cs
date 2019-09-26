using Alabo.Cloud.Shop.SuccessfulCases.Domains.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.SuccessfulCases.Domains.Repositories {

    public interface ICasesRepository : IRepository<Cases, ObjectId> {
    }
}