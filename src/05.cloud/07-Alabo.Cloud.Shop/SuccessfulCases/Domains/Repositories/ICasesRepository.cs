using MongoDB.Bson;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SuccessfulCases.Domains.Repositories {

    public interface ICasesRepository : IRepository<Cases, ObjectId> {
    }
}