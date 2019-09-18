using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.Province.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.Province.Domain.Services {

    public interface IProvinceService : IService<Entities.Province, ObjectId> {
    }
}