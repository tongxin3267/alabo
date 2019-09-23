using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Transfers.Domain.Entities;

namespace Alabo.App.Asset.Transfers.Domain.Repositories {

    public interface ITransferRepository : IRepository<Transfer, long> {
    }
}