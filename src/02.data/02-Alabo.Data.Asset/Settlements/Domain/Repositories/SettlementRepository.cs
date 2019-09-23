using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Settlements.Domain.Repositories;

namespace Alabo.App.Asset.Settlements.Domain.Repositories {

    public class SettlementRepository : RepositoryMongo<Settlement, long>, ISettlementRepository {

        public SettlementRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}