using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.AfterSale.Domain.Entities;

namespace Alabo.App.Shop.AfterSale.Domain.Services {

    public class AfterSaleService : ServiceBase<Entities.AfterSale, ObjectId>, IAfterSaleService {

        public AfterSaleService(IUnitOfWork unitOfWork, IRepository<Entities.AfterSale, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}