using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Withdraws.Domain.Entities;

namespace Alabo.App.Asset.Withdraws.Domain.Repositories {

    public interface IWithdrawRepository : IRepository<Withdraw, long> {
    }
}