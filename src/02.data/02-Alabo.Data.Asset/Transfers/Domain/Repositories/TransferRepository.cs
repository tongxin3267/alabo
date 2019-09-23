using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Transfers.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Transfers.Domain.Repositories;

namespace Alabo.App.Asset.Transfers.Domain.Repositories {

    public class TransferRepository : RepositoryMongo<Transfer, long>, ITransferRepository {

        public TransferRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}