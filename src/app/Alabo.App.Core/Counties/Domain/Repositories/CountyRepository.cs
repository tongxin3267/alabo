using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.County.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Agent.County.Domain.Repositories;

namespace Alabo.App.Agent.County.Domain.Repositories {

    public class CountyRepository : RepositoryMongo<Domain.Entities.County, ObjectId>, ICountyRepository {

        public CountyRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}