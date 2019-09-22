using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Agent.Province.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Agent.Province.Domain.Repositories;

namespace Alabo.App.Agent.Province.Domain.Repositories {

    public class ProvinceRepository : RepositoryMongo<Entities.Province, ObjectId>, IProvinceRepository {

        public ProvinceRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}