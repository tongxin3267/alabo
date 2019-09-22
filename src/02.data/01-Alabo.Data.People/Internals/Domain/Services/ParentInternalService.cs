using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Internal.Domain.Entities;

namespace Alabo.App.Agent.Internal.Domain.Services {

    public class ParentInternalService : ServiceBase<ParentInternal, ObjectId>, IParentInternalService {

        public ParentInternalService(IUnitOfWork unitOfWork, IRepository<ParentInternal, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}