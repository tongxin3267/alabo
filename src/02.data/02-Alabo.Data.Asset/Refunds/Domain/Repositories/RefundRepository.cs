using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Refunds.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Refunds.Domain.Repositories;

namespace Alabo.App.Asset.Refunds.Domain.Repositories {

    public class RefundRepository : RepositoryMongo<Refund, long>, IRefundRepository {

        public RefundRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}