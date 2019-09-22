using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Agent.Circle.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Agent.Circle.Domain.Services {

    public interface ICircleService : IService<Entities.Circle, ObjectId> {

        void Init();
    }
}