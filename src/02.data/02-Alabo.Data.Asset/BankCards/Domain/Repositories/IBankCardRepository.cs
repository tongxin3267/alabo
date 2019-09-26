using Alabo.App.Asset.BankCards.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Asset.BankCards.Domain.Repositories
{
    public interface IBankCardRepository : IRepository<BankCard, ObjectId>
    {
    }
}