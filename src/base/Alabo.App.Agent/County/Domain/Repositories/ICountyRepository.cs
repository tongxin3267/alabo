using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.County.Domain.Entities;

namespace Alabo.App.Agent.County.Domain.Repositories {

    public interface ICountyRepository : IRepository<Domain.Entities.County, ObjectId> {
    }
}