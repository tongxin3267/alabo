using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Store.Domain.Entities;

namespace Alabo.App.Shop.Store.Domain.Services {

    public class StoreService : ServiceBase<Entities.Store, ObjectId>, IStoreService {

        public StoreService(IUnitOfWork unitOfWork, IRepository<Entities.Store, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}