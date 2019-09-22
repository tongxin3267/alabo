using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.County.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.County.Domain.Services {

    public interface ICountyService : IService<Domain.Entities.County, ObjectId> {
    }
}