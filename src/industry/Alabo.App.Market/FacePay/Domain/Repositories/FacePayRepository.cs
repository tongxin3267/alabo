using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.FacePay.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.App.Market.FacePay.Domain.Repositories;

namespace Alabo.App.Market.FacePay.Domain.Repositories {

    public class FacePayRepository : RepositoryMongo<Entities.FacePay, ObjectId>, IFacePayRepository {

        public FacePayRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}