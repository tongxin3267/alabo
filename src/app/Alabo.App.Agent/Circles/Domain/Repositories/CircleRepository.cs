using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.Circle.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Agent.Circle.Domain.Repositories;

namespace Alabo.App.Agent.Circle.Domain.Repositories {

    public class CircleRepository : RepositoryMongo<Entities.Circle, ObjectId>, ICircleRepository {

        public CircleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}