using Alabo.App.Asset.BankCards.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Asset.BankCards.Domain.Repositories {

    public class BankCardRepository : RepositoryMongo<BankCard, ObjectId>, IBankCardRepository {

        public BankCardRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}