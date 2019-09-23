using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Market.FacePay.Domain.Entities;

namespace Alabo.App.Market.FacePay.Domain.Services {

    public class FacePayService : ServiceBase<Entities.FacePay, ObjectId>, IFacePayService {

        public FacePayService(IUnitOfWork unitOfWork, IRepository<Entities.FacePay, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}