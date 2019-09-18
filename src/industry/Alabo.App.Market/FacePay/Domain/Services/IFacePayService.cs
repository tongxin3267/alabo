using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.FacePay.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.FacePay.Domain.Services {

    public interface IFacePayService : IService<Entities.FacePay, ObjectId> {
    }
}