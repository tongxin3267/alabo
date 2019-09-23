using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Settlements.Domain.Entities;

namespace Alabo.App.Asset.Settlements.Domain.Repositories {

    public interface ISettlementRepository : IRepository<Settlement, long> {
    }
}