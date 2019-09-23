using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Asset.Withraws.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Asset.Withraws.Domain.Repositories;

namespace Alabo.App.Asset.Withraws.Domain.Repositories {

    public class WithrawRepository : RepositoryMongo<Withraw, long>, IWithrawRepository {

        public WithrawRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}