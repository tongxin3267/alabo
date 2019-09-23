using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Settlements.Domain.Entities;

namespace Alabo.App.Asset.Settlements.Domain.Services {

    public class SettlementService : ServiceBase<Settlement, long>, ISettlementService {

        public SettlementService(IUnitOfWork unitOfWork, IRepository<Settlement, long> repository) : base(unitOfWork, repository) {
        }
    }
}