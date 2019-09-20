using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Internal.Domain.Entities;

namespace Alabo.App.Agent.Internal.Domain.Repositories {

    public interface IParentInternalRepository : IRepository<ParentInternal, ObjectId> {
    }
}