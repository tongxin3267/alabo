using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.ShareHolders.Domain.Entities;

namespace Alabo.App.Agent.ShareHolders.Domain.Services {

    public class ShareHolderService : ServiceBase<ShareHolder, ObjectId>, IShareHolderService {

        public ShareHolderService(IUnitOfWork unitOfWork, IRepository<ShareHolder, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}