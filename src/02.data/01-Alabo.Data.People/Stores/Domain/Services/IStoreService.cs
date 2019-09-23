using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.Store.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Store.Domain.Services {

    public interface IStoreService : IService<Entities.Store, ObjectId> {
    }
}