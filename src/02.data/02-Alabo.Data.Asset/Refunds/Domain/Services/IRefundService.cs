using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Asset.Refunds.Domain.Services {

    public interface IRefundService : IService<Refund, long> {
    }
}