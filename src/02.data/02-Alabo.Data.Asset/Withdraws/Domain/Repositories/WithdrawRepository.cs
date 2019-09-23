using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Withdraws.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Withdraws.Domain.Repositories;

namespace Alabo.App.Asset.Withdraws.Domain.Repositories {

    public class WithdrawRepository : RepositoryMongo<Withdraw, long>, IWithdrawRepository {

        public WithdrawRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}