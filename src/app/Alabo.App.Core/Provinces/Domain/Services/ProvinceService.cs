using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Province.Domain.Entities;

namespace Alabo.App.Agent.Province.Domain.Services {

    public class ProvinceService : ServiceBase<Entities.Province, ObjectId>, IProvinceService {

        public ProvinceService(IUnitOfWork unitOfWork, IRepository<Entities.Province, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}