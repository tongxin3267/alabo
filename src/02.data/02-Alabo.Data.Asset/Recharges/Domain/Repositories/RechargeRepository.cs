using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Recharges.Domain.Repositories;

namespace Alabo.App.Asset.Recharges.Domain.Repositories {

    public class RechargeRepository : RepositoryMongo<Recharge, long>, IRechargeRepository {

        public RechargeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}