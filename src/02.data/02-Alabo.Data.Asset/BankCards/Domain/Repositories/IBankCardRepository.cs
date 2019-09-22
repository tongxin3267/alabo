using MongoDB.Bson;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public interface IBankCardRepository : IRepository<BankCard, ObjectId> {
    }
}