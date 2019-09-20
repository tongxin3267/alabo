using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.County.Domain.Entities;

namespace Alabo.App.Agent.County.Domain.Services {

    public class CountyService : ServiceBase<Domain.Entities.County, ObjectId>, ICountyService {

        public CountyService(IUnitOfWork unitOfWork, IRepository<Domain.Entities.County, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}