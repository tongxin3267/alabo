using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Asset.Settlements.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Asset.Settlements.Domain.Services {

    public interface ISettlementService : IService<Settlement, long> {
    }
}