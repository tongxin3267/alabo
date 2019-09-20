using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.Internal.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Agent.Internal.Domain.Repositories;

namespace Alabo.App.Agent.Internal.Domain.Repositories {

    public class ParentInternalRepository : RepositoryMongo<ParentInternal, ObjectId>, IParentInternalRepository {

        public ParentInternalRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}