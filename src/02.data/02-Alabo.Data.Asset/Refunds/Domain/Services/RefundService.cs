using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Asset.Refunds.Domain.Entities;

namespace Alabo.App.Asset.Refunds.Domain.Services {

    public class RefundService : ServiceBase<Refund, long>, IRefundService {

        public RefundService(IUnitOfWork unitOfWork, IRepository<Refund, long> repository) : base(unitOfWork, repository) {
        }
    }
}