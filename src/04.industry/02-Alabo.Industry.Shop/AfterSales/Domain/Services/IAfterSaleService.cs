using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.AfterSale.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Services {

    public interface IAfterSaleService : IService<Entities.AfterSale, ObjectId> {
    }
}