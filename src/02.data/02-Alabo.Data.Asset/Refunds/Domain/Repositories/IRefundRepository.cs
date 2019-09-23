using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Refunds.Domain.Entities;

namespace Alabo.App.Asset.Refunds.Domain.Repositories {

    public interface IRefundRepository : IRepository<Refund, long> {
    }
}