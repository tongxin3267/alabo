using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Recharges.Domain.Entities;

namespace Alabo.App.Asset.Recharges.Domain.Repositories {

    public interface IRechargeRepository : IRepository<Recharge, long> {
    }
}