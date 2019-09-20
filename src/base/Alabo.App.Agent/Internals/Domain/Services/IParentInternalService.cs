using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.Internal.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.Internal.Domain.Services {

    public interface IParentInternalService : IService<ParentInternal, ObjectId> {
    }
}