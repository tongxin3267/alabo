using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Circle.Domain.Entities;

namespace Alabo.App.Agent.Circle.Domain.Repositories {

    public interface ICircleRepository : IRepository<Entities.Circle, ObjectId> {
    }
}