using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Province.Domain.Entities;

namespace Alabo.App.Agent.Province.Domain.Repositories {

    public interface IProvinceRepository : IRepository<Entities.Province, ObjectId> {
    }
}