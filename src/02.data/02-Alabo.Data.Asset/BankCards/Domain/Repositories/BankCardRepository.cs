using MongoDB.Bson;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Finance.Domain.Repositories {

    public class BankCardRepository : RepositoryMongo<BankCard, ObjectId>, IBankCardRepository {

        public BankCardRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}